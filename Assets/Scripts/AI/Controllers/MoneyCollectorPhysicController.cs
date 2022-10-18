using System;
using System.Collections.Generic;
using AI.Subscribers;
using Enums;
using UnityEngine;

namespace AI.Controllers
{
    public class MoneyCollectorPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        [SerializeField] private MoneyCollector moneyCollector;

        #endregion

        #region Private Variables


        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(StackTypes.Money.ToString()))
            {
                //doSomething
            }
        }
    }
}