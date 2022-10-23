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

        private void BulletMove()
        {
            rigidbody.AddRelativeForce(Vector3.forward*5,ForceMode.Force);
        }

        private void FixedUpdate()
        {
            BulletMove();
        }
    }
}