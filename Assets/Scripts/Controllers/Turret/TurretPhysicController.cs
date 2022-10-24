using UnityEngine;

namespace Controllers.Turret
{
    [RequireComponent(typeof(BoxCollider))]
    public class TurretPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BoxCollider boxCollider;

        #endregion

        #endregion

        public void CloseCollider()
        {
            boxCollider.enabled = false;
        }
    }
}