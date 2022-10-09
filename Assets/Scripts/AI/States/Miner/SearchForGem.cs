using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AI.States.Miner
{
    public class SearchForGem: IAIStates
    {
        #region Self Variables

        #region Private Variables

        private readonly Subscribers.Miner _miner;
        private readonly List<Transform> _mineTransforms;

        #endregion

        #endregion

        public SearchForGem(Subscribers.Miner miner,List<Transform> mineTrans)
        {
            _miner = miner;
            _mineTransforms = mineTrans;
        }
        public void Tick()
        {
            _miner.Target = ChooseOfSuitMine(_mineTransforms);
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }

        private Transform ChooseOfSuitMine(List<Transform> mineTransforms)
        {
            var index = Random.Range(0, mineTransforms.Count);
            var target = mineTransforms[index];
            return target;
        } 
    }
}