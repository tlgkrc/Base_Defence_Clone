using System;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private StackManager stackManager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            var gO = other.transform.parent.gameObject;
            if (other.CompareTag("MineStack"))
            {
                StackSignals.Instance.onClearStaticStack?.Invoke(transform);
            }
            else if (other.CompareTag("Hostage"))
            {
                StackSignals.Instance.onAddHostageToStack?.Invoke(other.transform.parent.gameObject);
                BaseSignals.Instance.onPlaceNewHostage?.Invoke(other.transform);
            }
            else if(other.CompareTag("Money"))
            {
                StackSignals.Instance.onAddStack?.Invoke(transform.parent.GetInstanceID(),gO);
            }
            else if (other.CompareTag("Gate") && stackManager.transform.childCount > 0)
            {
                StackSignals.Instance.onClearDynamicStack?.Invoke(manager.transform.GetInstanceID());
            }
            else if(other.CompareTag("Turret"))
            {
                manager.PlayerAtTurret(other.transform);
                BaseSignals.Instance.onSetPlayerToTurretShooter?.Invoke();
                CoreGameSignals.Instance.onSetCameraState?.Invoke(CameraStates.TurretCam);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Turret"))
            {
                BaseSignals.Instance.onReleasePlayer?.Invoke();
                CoreGameSignals.Instance.onSetCameraState?.Invoke(CameraStates.LevelCam);
                manager.ReleaseFromTurret();
            }
        }
    }
}