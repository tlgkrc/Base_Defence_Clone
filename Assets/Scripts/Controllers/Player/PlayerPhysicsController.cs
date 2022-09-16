using System;
using System.Collections;
using Signals;
using UnityEngine;
using Managers;
using Enums;
using UnityEditor.VersionControl;
using Task = System.Threading.Tasks.Task;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private ParticleSystem currentParticle;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}