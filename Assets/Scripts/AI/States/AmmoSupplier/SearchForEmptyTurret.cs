using System.Collections.Generic;
using Enums.Animations;
using Signals;
using UnityEngine;

namespace AI.States.AmmoSupplier
{
    public class SearchForEmptyTurret: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.AmmoSupplier _ammoSupplier;
        private readonly Animator _animator;
        private Transform _target;

        #endregion

        #endregion

        public SearchForEmptyTurret(Subscribers.AmmoSupplier ammoSupplier,Animator animator)
        {
            _ammoSupplier = ammoSupplier;
            _animator = animator;
        }

        public void Tick()
        {
            _ammoSupplier.Target = GetSuitableTurret();
            _ammoSupplier.TakeAmmoBoxes();
        }

        public void OnEnter()
        {
            _animator.SetTrigger(AmmoWorkerAnimStates.Idle.ToString());
        }

        public void OnExit()
        {
            
        }

        private Transform GetSuitableTurret()
        {
            var newTransform = BaseSignals.Instance.onSetAmmoStockTransform?.Invoke();
            _ammoSupplier.TurretAmmoTransforms.Add(newTransform);
            var minValue = 0;
            
            for (var i = 0; i < _ammoSupplier.TurretAmmoTransforms.Count; i++)
            {
                if (i == 0)
                {
                    minValue = _ammoSupplier.TurretAmmoTransforms[0].childCount;
                    _target = _ammoSupplier.TurretAmmoTransforms[0];
                    _ammoSupplier.TargetIndex = i;
                }
                else
                {
                    if (minValue <= _ammoSupplier.TurretAmmoTransforms[i].childCount) continue;
                    minValue = _ammoSupplier.TurretAmmoTransforms[i].childCount;
                    _target = _ammoSupplier.TurretAmmoTransforms[i];
                    _ammoSupplier.TargetIndex = i;
                }
            }
            return _target;
        }
    }
}