using System;
using DG.Tweening;
using UnityEngine;

namespace Controllers.Area
{
    public class Gate : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject gate;

        #endregion

        private Vector3 _endValue = new Vector3(0, 0, -60);
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("MoneyWorker"))
            {
                gate.transform.DORotate(_endValue, .2f, RotateMode.WorldAxisAdd);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("MoneyWorker"))
            {
                gate.transform.DORotate(-_endValue,  .2f, RotateMode.WorldAxisAdd);
            }
        }
    }
}