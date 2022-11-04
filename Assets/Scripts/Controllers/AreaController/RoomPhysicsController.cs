using Managers;
using UnityEngine;

namespace Controllers.AreaController
{
    
    public class RoomPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private RoomManager roomManager;
        [SerializeField] private SpriteRenderer spriteRenderer;

        #endregion

        #region Private Variables

        private float _value = 0;
        private int _currenValue;
        private static readonly int Arc2 = Shader.PropertyToID("_Arc2");
        private Material _material;

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
                roomManager.BuyRoom();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
               roomManager.StopBuying();
            }
        }

        public void SetRadialVisual(int paidAmount,int costOfRoom)
        {
            _value = (1-(paidAmount/costOfRoom)) * 360;
            _material.SetFloat(Arc2,_value);
        }
    }
}