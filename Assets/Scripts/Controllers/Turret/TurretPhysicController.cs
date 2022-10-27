using Managers;
using UnityEngine;

namespace Controllers.Turret
{
    [RequireComponent(typeof(BoxCollider))]
    public class TurretPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TurretManager turretManager;

        #endregion

        #region Private Variables

        private Material _material;
        private float _value;
        private static readonly int Arc2 = Shader.PropertyToID("_Arc2");

        #endregion

        #endregion
        
        private void Awake()
        {
            _material = spriteRenderer.material;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                turretManager.BuyTurretWorker();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                turretManager.StopBuying();
            }
        }

        public void SetRadialVisual(int paidAmount,int costOfRoom)
        {
            _value = (1-(paidAmount/costOfRoom)) * 360;
            _material.SetFloat(Arc2,_value);
        }

        public void CloseCollider()
        {
            boxCollider.enabled = false;
        }
    }
}