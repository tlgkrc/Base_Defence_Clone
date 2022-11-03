using System;
using Managers;
using UnityEngine;

namespace Controllers.AreaController
{
    public class BuyAmmoWorkerPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BuyWorkersManager buyWorkersManager;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        #endregion

        #region Private Variables
        
        private Material _material;
        private static readonly int Arc2 = Shader.PropertyToID("_Arc2");
        private int _currenValue;
        private float _value = 0;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                buyWorkersManager.BuyAmmoWorker();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                buyWorkersManager.StopAmmoWorkerBuying();
            }
            
        }
    }
}