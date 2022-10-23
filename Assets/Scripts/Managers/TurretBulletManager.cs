using System;
using UnityEngine;

namespace Managers
{
    public class TurretBulletManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;

        #endregion

        #endregion
        private void Awake()
        {
            rigidbody.AddForce(Vector3.forward*2,ForceMode.Impulse);
        }
    }
}