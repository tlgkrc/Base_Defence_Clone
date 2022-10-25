using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.AreaController
{
    
    public class RoomPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private RoomManager roomManager;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private int speed;

        #endregion

        #region Private Variables

        private Material _material;
        private static readonly int Arc2 = Shader.PropertyToID("_Arc2");
        private int _currenValue;
        private float _value = 0;

        #endregion

        #endregion

        private void Awake()
        {
            _material = spriteRenderer.material;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _value += Time.time * speed;
                _currenValue = (int)(360 - _value);
                if (_currenValue > 0)
                {
                    _material.SetFloat(Arc2, _currenValue);
                }
                else
                {
                    transform.parent.gameObject.SetActive(false);
                }
            }
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

        public void SetRadialVisual(int moneyToPay,int costOfRoom)
        {
            float newValue = (float)moneyToPay / costOfRoom;
            _material.SetFloat(Arc2,newValue);
        }
    }
}