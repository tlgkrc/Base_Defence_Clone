using System;
using System.Collections.Generic;
using AI.Controllers;
using AI.States;
using AI.States.Enemy;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Enums.Animations;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Subscribers
{
    public class Enemy : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public Transform Target { get; set; }

        #endregion

        #region Serialized Variables

        [SerializeField] private Animator animator;
        [SerializeField] private EnemyPhysicController physicController;
        [SerializeField] private EnemyTypes enemyType;

        #endregion

        #region Private Variables

        private bool _playerInRange = false;
        private int _health;
        private List<Transform> _baseTargetTransforms = new List<Transform>();
        private EnemyGOData _enemyGoData;
        private AIStateMachine _aiStateMachine;
        
        #endregion

        #endregion

        private void Awake()
        {
            _enemyGoData = GetEnemyData();
            _health = _enemyGoData.Health;
            _aiStateMachine = new AIStateMachine();
            
            var navMeshAgent = GetComponent<NavMeshAgent>();
            var searchBaseTarget = new SearchForBaseTarget(this, _baseTargetTransforms,animator);
            var moveToBaseTarget = new MoveToBaseTarget(this, navMeshAgent,_enemyGoData,animator);
            var attackToWall = new AttackToWall(this,navMeshAgent,animator);
            var moveToPlayer = new MoveToPlayer(this,navMeshAgent,_enemyGoData,animator);
            var die = new Die(this, animator, navMeshAgent);

            At(searchBaseTarget,moveToBaseTarget,HasTarget());
            At(moveToBaseTarget,attackToWall,ReachedBaseTarget());
            
            _aiStateMachine.AddAnyTransition(moveToPlayer,EnemyInRange());
           
            At(moveToPlayer,attackToWall, () => _playerInRange == true && _health>=0);
            At(moveToPlayer, searchBaseTarget, () => _playerInRange == false && _health >= 0);

            _aiStateMachine.AddAnyTransition(die,IsDead());
           
            At(die,searchBaseTarget,() => _health>0);
            
            _aiStateMachine.SetState(searchBaseTarget);

            Func<bool> HasTarget() => () => Target != null;
            Func<bool> ReachedBaseTarget() => () => 
                Target != null && Vector3.Distance(transform.position, Target.position) <= 1f;
            Func<bool> EnemyInRange() => () =>
                _playerInRange && _health > 0 && Vector3.Distance(Target.position, transform.position) > 1f;
            Func<bool> IsDead() => () => _health <= 0;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AISignals.Instance.onSetEnemyTarget += OnSetEnemyTarget;
        }

        private void UnsubscribeEvents()
        {
            AISignals.Instance.onSetEnemyTarget -= OnSetEnemyTarget;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            var transforms = BaseSignals.Instance.onSetBaseTargetTransforms?.Invoke();
            foreach (var value in transforms)
            {
                _baseTargetTransforms.Add(value);
            }
        }

        private void At(IAIStates to, IAIStates from, Func<bool> condition)
        {
            _aiStateMachine.AddTransition(to, from, condition);
        }

        private EnemyGOData GetEnemyData()
        {
            return Resources.Load<CD_EnemyData>("Data/CD_EnemyData").Data.EnemyDatas[enemyType];
        }

        private void Update()
        {
            _aiStateMachine.Tick();
        }

        public void SetPlayerRange(bool playerInRange)
        {
            _playerInRange = playerInRange;
        }

        public void Hit(bool isPlayer,int weaponDamage)
        {
            int? damage = 0;
            if (isPlayer)
            {
                damage = weaponDamage;
            }
            else
            {
                 damage = CoreGameSignals.Instance.onSetTurretBulletDamage?.Invoke();
            }
            
            _health = (int)(_health - damage);
            
            if (_health<= 0)
            {
                CoreGameSignals.Instance.onDieEnemy?.Invoke(this.gameObject);
                physicController.gameObject.SetActive(false);
                animator.SetTrigger(EnemyAnimTypes.Die.ToString());
                
                Die();
                for (int i = 0; i < 3; i++)
                {
                    var money =PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Money.ToString(), transform);
                    money.transform.position = this.transform.position;
                }
            }
            else
            {
                animator.SetTrigger(EnemyAnimTypes.Hit.ToString());
            }
        }

        private void Die()
        {
            Invoke(nameof(Dead), 3f);
        }

        private void Dead()
        {
            PoolSignals.Instance.onReleasePoolObject?.Invoke(enemyType.ToString(), gameObject);
            _health = _enemyGoData.Health;
            physicController.gameObject.SetActive(true);
        }

        public void AttackPlayer()
        {
            CoreGameSignals.Instance.onUpdatePlayerHealth?.Invoke(_enemyGoData.Damage);
        }

        private void OnSetEnemyTarget()
        {
            Target = null;
        }
    }
}