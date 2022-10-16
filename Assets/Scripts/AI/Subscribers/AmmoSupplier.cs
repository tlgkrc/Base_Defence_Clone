using System;
using System.Collections.Generic;
using AI.States;
using AI.States.AmmoSupplier;
using Data;
using Data.UnityObject;
using Data.ValueObject;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Subscribers
{
    public class AmmoSupplier : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Transform Target { get; set; }

        #endregion

        #region Serialized Variables
        
        [SerializeField] private List<Transform> turretTransforms;
        [SerializeField] private Transform ammoDepot;
        [SerializeField] private StackManager stackManager;

        #endregion

        #region Private Variables

        private AIStateMachine _aiStateMachine;
        private AmmoSupplierData _ammoSupplierData;

        #endregion

        #endregion

        private void Awake()
        {
            _aiStateMachine = new AIStateMachine();
            _ammoSupplierData = GetAmmoSupplierData();
            var navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.speed = _ammoSupplierData.SupplierSpeed;
            
            var moveAmmoDepot = new MoveToAmmoDepot(this,navMeshAgent,ammoDepot);
            var searchForEmptyTurret = new SearchForEmptyTurret(this,turretTransforms);
            var moveToTurret = new MoveToTurret(this,navMeshAgent);

            At(moveAmmoDepot, searchForEmptyTurret, StackIsEmpty());
            At(searchForEmptyTurret,moveToTurret,StackIsFull());
            At(moveToTurret,moveAmmoDepot,HasAmmoDelivered());
            
            _aiStateMachine.SetState(moveAmmoDepot);

            Func<bool> StackIsEmpty() => () => stackManager.transform.childCount <= 0
                                               && Vector3.Distance(transform.position,ammoDepot.position)<=2f;
            Func<bool> StackIsFull() => () => _ammoSupplierData.MaxStackCount == stackManager.transform.childCount;
            Func<bool> HasAmmoDelivered() => () => Vector3.Distance(transform.position,Target.transform.position)<=1.2f 
            && stackManager.transform.childCount <=0;
        }
        
        private void At(IAIStates to, IAIStates from, Func<bool> condition)
        {
            _aiStateMachine.AddTransition(to, from, condition);
        }
        
        private AmmoSupplierData GetAmmoSupplierData()
        {
            return Resources.Load<CD_AmmoSupplier>("Data/CD_AmmoSupplier").AmmoSupplierData;
        }

        private void Update()
        {
            _aiStateMachine.Tick();
        }
    }
}