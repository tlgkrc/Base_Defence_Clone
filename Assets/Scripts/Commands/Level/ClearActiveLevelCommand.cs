using UnityEngine;

namespace Commands
{
    public class ClearActiveLevelCommand
    {
        public void ClearActiveLevel(Transform levelHolder)
        {
            foreach (Transform child in levelHolder)
            {
                Object.Destroy(child.gameObject);
            }
            
        }
    }
}