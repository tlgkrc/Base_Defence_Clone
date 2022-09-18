using UnityEngine;

namespace Controllers.Area
{
    public class StageController : MonoBehaviour
    {
        public void UpdateStageArea(GameObject gameObject)
        {
            var siblingIndex = gameObject.transform.GetSiblingIndex();
            gameObject.transform.parent.GetChild(siblingIndex).gameObject.SetActive(false);
            gameObject.transform.parent.GetChild(siblingIndex+1).gameObject.SetActive(true);
            
        }
    }
}