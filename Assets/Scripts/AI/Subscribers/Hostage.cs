using System;
using AI.Controllers;
using Signals;
using UnityEngine;

namespace AI.Subscribers
{
    public class Hostage : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables


        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void Awake()
        {

        }
        #region Subscription Events

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

        }

        private void UnsubscribeEvents()
        {
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

    }
}