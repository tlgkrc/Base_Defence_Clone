using System.Collections.Generic;
using Enums.Animations;
using UnityEngine;

namespace AI.States.AmmoSupplier
{
    public class SearchForEmptyTurret: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.AmmoSupplier _ammoSupplier;
        private readonly List<Transform> _ammoStockTransforms;
        private readonly Animator _animator;

        #endregion

        #endregion

        public SearchForEmptyTurret(Subscribers.AmmoSupplier ammoSupplier,List<Transform> ammoStockTransforms,Animator animator)
        {
            _ammoSupplier = ammoSupplier;
            _ammoStockTransforms = ammoStockTransforms;
            _animator = animator;
        }

        public void Tick()
        {
            _ammoSupplier.Target = ChooseOfSuitMine();
            _ammoSupplier.TakeAmmoBoxes();
        }

        public void OnEnter()
        {
            _animator.SetTrigger(AmmoWorkerAnimStates.Idle.ToString());
        }

        public void OnExit()
        {
            
        }
        private Transform ChooseOfSuitMine()
        {
            var index = Random.Range(0, _ammoStockTransforms.Count);
            _ammoSupplier.TargetIndex = index;
            var target = _ammoStockTransforms[index];
            return target;
        } 
    }
}