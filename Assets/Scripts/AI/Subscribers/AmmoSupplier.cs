using System;
using System.Collections.Generic;
using AI.States;
using AI.States.AmmoSupplier;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Managers;
using Signals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Subscribers
{
    public class AmmoSupplier : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Transform Target { get; set; }
        public int TargetIndex { get; set; }

        #endregion

        #region Serialized Variables

        [SerializeField] private StackManager stackManager;
        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        private AIStateMachine _aiStateMachine;
        private AmmoSupplierData _ammoSupplierData;
        private List<Transform> _turretAmmoTransforms;
        private Transform _ammoDepot;

        #endregion

        #endregion

        private void Awake()
        {
            _aiStateMachine = new AIStateMachine();
            _ammoSupplierData = GetAmmoSupplierData();
            var navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.speed = _ammoSupplierData.SupplierSpeed;
            
            var moveAmmoDepot = new MoveToAmmoDepot(this,navMeshAgent,_ammoDepot,animator);
            var searchForEmptyTurret = new SearchForEmptyTurret(this,_turretAmmoTransforms,animator);
            var moveToTurret = new MoveToTurret(this,navMeshAgent,animator);
            var deliverAmmo = new DeliverAmmoBoxes(this);

            At(moveAmmoDepot, searchForEmptyTurret, StackIsEmpty());
            At(searchForEmptyTurret,moveToTurret,StackIsFull());
            At(moveToTurret,deliverAmmo,ReachedAmmoStock());
            At(deliverAmmo,moveAmmoDepot,AmmoHasDelivered());

            _aiStateMachine.SetState(moveAmmoDepot);

            Func<bool> StackIsEmpty() => () => stackManager.transform.childCount <= 0
                                               && Vector3.Distance(transform.position,_ammoDepot.position)<=2f;
            Func<bool> StackIsFull() => () => _ammoSupplierData.MaxStackCount == stackManager.transform.childCount;
            Func<bool> ReachedAmmoStock() =>
                () => Vector3.Distance(transform.position, Target.transform.position) <= 2f;
            Func<bool> AmmoHasDelivered() => () => stackManager.transform.childCount <= 0;
        }

        private void At(IAIStates to, IAIStates from, Func<bool> condition)
        {
            _aiStateMachine.AddTransition(to, from, condition);
        }

        private void Start()
        {
            _turretAmmoTransforms = BaseSignals.Instance.onSetAmmoStockTransforms?.Invoke();
            _ammoDepot = BaseSignals.Instance.onSetAmmoDepotTransform?.Invoke();
        }

        private AmmoSupplierData GetAmmoSupplierData()
        {
            return Resources.Load<CD_AmmoSupplier>("Data/CD_AmmoSupplier").AmmoSupplierData;
        }

        private void Update()
        {
            _aiStateMachine.Tick();
        }

        public void TakeAmmoBoxes()
        {
            for (int i = 0; i < _ammoSupplierData.MaxStackCount; i++)
            {
                var gO = PoolSignals.Instance.onGetPoolObject(PoolTypes.BulletBox.ToString(), this.transform);;
                StackSignals.Instance.onAddStack?.Invoke(transform.GetInstanceID(),gO);
            }
        }

        public void DeliverAmmo(int index)
        {
            StackSignals.Instance.onTransferBetweenStacks?.
                Invoke(transform.GetInstanceID(),stackManager,_turretAmmoTransforms[index].GetComponent<StackManager>());
            StackSignals.Instance.onDeliverAmmoBox?.
                Invoke(_turretAmmoTransforms[index].GetComponent<StackManager>().transform.GetInstanceID(),_ammoSupplierData.MaxStackCount);
        }
    }
}