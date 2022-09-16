using UnityEngine;

namespace ExampleAIStateMachine.Scripts
{
    public class StuffDropper : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CollectableResources prefab;

        #endregion

        #endregion

        public void Drop(int collected,Vector3 position)
        {
            var resource = Instantiate(prefab, position, Quaternion.identity);
            resource.SetAvailable(collected);
        }
    }
}