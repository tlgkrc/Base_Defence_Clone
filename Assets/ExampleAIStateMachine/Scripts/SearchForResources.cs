using System.Linq;
using UnityEngine;


namespace ExampleAIStateMachine.Scripts
{
    public class SearchForResources:IAIState
    {
        #region Self Variables

        #region Private Variables

        private readonly Collector _collector;

        #endregion

        #endregion

        public SearchForResources(Collector collector)
        {
            _collector = collector;
        }
        public void Tick()
        {
            _collector.Target = ChooseOneOfTheNearestResources(1);
        }

        private CollectableResources ChooseOneOfTheNearestResources(int pickFromNearest)
        {
            return Object.FindObjectsOfType<CollectableResources>()
                .OrderBy(t=>Vector3.Distance(_collector.transform.position,t.transform.position))
                .Where(t => t.IsDepleted == false)
                .Take(pickFromNearest)
                .OrderBy(t => Random.Range(0, int.MaxValue))
                .FirstOrDefault();
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
          
        }
    }
}