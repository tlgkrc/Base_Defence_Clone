using System;
using System.Collections.Generic;
using AI.States;
using AI.States.AmmoSupplier;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Subscribers
{
    public class AmmoSupplier : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public int TargetIndex { get; set; }
        public Transform Target { get; set; }
        public List<Transform> TurretAmmoTransforms = new List<Transform>();

        #endregion

        #region Serialized Variables

        [SerializeField] private Animator animator;
        [SerializeField] private StackManager stackManager;
        
        #endregion

        #region Private Variables

        private Transform _ammoDepot;
        private Transform _target;
        private AIStateMachine _aiStateMachine;
        private AmmoSupplierData _ammoSupplierData;

        #endregion

        #endregion

        #region Subscription Events

        private void OnEnable()
        {
            GetReferences();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AISignals.Instance.onSetOpenedTurret += OnSetOpenedTurret;
        }

        private void UnsubscribeEvents()
        {
            AISignals.Instance.onSetOpenedTurret -= OnSetOpenedTurret;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            Init();
        }
        private void Update()
        {
            _aiStateMachine.Tick();
        }
        
        private void GetReferences()
        {
            _ammoDepot = BaseSignals.Instance.onSetAmmoDepotTransform?.Invoke();
        }

        private void Init()
        {
            _aiStateMachine = new AIStateMachine();
            _ammoSupplierData = GetAmmoSupplierData();
            
            var navMeshAgent = GetComponent<NavMeshAgent>();
            var moveAmmoDepot = new MoveToAmmoDepot(this, navMeshAgent, _ammoDepot, animator);
            var searchForEmptyTurret = new SearchForEmptyTurret(this, animator);
            var moveToTurret = new MoveToTurret(this, navMeshAgent, animator);
            var deliverAmmo = new DeliverAmmoBoxes(this);

            At(moveAmmoDepot, searchForEmptyTurret, StackIsEmpty());
            At(searchForEmptyTurret, moveToTurret, StackIsFull());
            At(moveToTurret, deliverAmmo, ReachedAmmoStock());
            At(deliverAmmo, moveAmmoDepot, AmmoHasDelivered());

            _aiStateMachine.SetState(moveAmmoDepot);

            Func<bool> StackIsEmpty() => () => stackManager.transform.childCount <= 0
                                               && Vector3.Distance(transform.position, _ammoDepot.position) <= 2f;
            Func<bool> StackIsFull() => () => _ammoSupplierData.MaxStackCount == stackManager.transform.childCount;
            Func<bool> ReachedAmmoStock() =>
                () => Vector3.Distance(transform.position, Target.position) <= 2f;
            Func<bool> AmmoHasDelivered() => () => stackManager.transform.childCount <= 0;
            
            navMeshAgent.speed = _ammoSupplierData.SupplierSpeed;
        }
        private void At(IAIStates to, IAIStates from, Func<bool> condition)
        {
            _aiStateMachine.AddTransition(to, from, condition);
        }

        private AmmoSupplierData GetAmmoSupplierData()
        {
            return Resources.Load<CD_AmmoSupplier>("Data/CD_AmmoSupplier").AmmoSupplierData;
        }

        public void TakeAmmoBoxes()
        {
            for (int i = 0; i < _ammoSupplierData.MaxStackCount; i++)
            {
                var gO = PoolSignals.Instance.onGetPoolObject(PoolTypes.BulletBox.ToString(), this.transform);
                StackSignals.Instance.onAddStack?.Invoke(transform.GetInstanceID(),gO);
            }
        }

        public void DeliverAmmo(int index)
        {
            var manager = TurretAmmoTransforms[index].GetComponent<StackManager>();
            StackSignals.Instance.onTransferBetweenStacks?.Invoke(manager.GetInstanceID(),stackManager,manager);
            StackSignals.Instance.onDeliverAmmoBox?.
                Invoke(manager.transform.GetInstanceID(),_ammoSupplierData.MaxStackCount);
        }

        private void OnSetOpenedTurret(Transform newTurret)
        {
            TurretAmmoTransforms.Add(newTurret);
        }
    }
}