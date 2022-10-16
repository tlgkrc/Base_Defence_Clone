using System.Collections.Generic;
using UnityEngine;

namespace AI.States.AmmoSupplier
{
    public class SearchForEmptyTurret: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.AmmoSupplier _ammoSupplier;
        private readonly List<Transform> _ammoStockTransforms;

        #endregion

        #endregion

        public SearchForEmptyTurret(Subscribers.AmmoSupplier ammoSupplier,List<Transform> ammoStockTransforms)
        {
            _ammoSupplier = ammoSupplier;
            _ammoStockTransforms = ammoStockTransforms;
        }

        public void Tick()
        {
            _ammoSupplier.Target = ChooseOfSuitMine();
        }

        public void OnEnter()
        {
            Debug.Log("searching");
        }

        public void OnExit()
        {
            
        }
        private Transform ChooseOfSuitMine()
        {
            var index = Random.Range(0, _ammoStockTransforms.Count);
            var target = _ammoStockTransforms[index];
            return target;
        } 
    }
}