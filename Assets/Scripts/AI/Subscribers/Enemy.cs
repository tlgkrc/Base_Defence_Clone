using System;
using System.Collections.Generic;
using AI.States;
using AI.States.Enemy;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Sirenix.OdinInspector;
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

        [SerializeField] private EnemyTypes enemyType;
        [SerializeField] private List<Transform> baseTargetTransforms = new List<Transform>();
        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        [ShowInInspector] private EnemyGOData _enemyGoData;
        private AIStateMachine _aiStateMachine;
        private bool _playerInRange = false;
        #endregion

        #endregion

        private void Awake()
        {
            _enemyGoData = GetEnemyData();
            _aiStateMachine = new AIStateMachine();
            var navMeshAgent = GetComponent<NavMeshAgent>();

            var searchBaseTarget = new SearchForBaseTarget(this, baseTargetTransforms,animator);
            var moveToBaseTarget = new MoveToBaseTarget(this, navMeshAgent,_enemyGoData,animator);
            var attackToWall = new AttackToWall(this,animator);
            var moveToPlayer = new MoveToPlayer(this,navMeshAgent,_enemyGoData,animator);
            
            At(searchBaseTarget,moveToBaseTarget,HasTarget());
            At(moveToBaseTarget,attackToWall,ReachedBaseTarget());
            
            _aiStateMachine.AddAnyTransition(moveToPlayer,EnemyInRange());
            At(moveToPlayer,searchBaseTarget, () => _playerInRange == false);

            _aiStateMachine.SetState(searchBaseTarget);

            Func<bool> HasTarget() => () => Target != null;
            Func<bool> ReachedBaseTarget() => () => 
                Target != null && Vector3.Distance(transform.position, Target.position) <= 1f;
            Func<bool> EnemyInRange() => () => _playerInRange;
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
    }
}