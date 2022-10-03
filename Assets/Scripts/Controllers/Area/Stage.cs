using UnityEngine;

namespace Controllers.Area
{
    public class Stage : MonoBehaviour
    {
        public void UpdateStageArea(GameObject gameObject)
        {
            var siblingIndex = gameObject.transform.GetSiblingIndex();
            var parent = gameObject.transform.parent;
            parent.GetChild(siblingIndex).gameObject.SetActive(false);
            parent.GetChild(siblingIndex+1).gameObject.SetActive(true);
            
        }
    }
}