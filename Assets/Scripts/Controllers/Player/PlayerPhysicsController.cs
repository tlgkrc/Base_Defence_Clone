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
        [SerializeField] private StackManager moneyStackManager;
        [SerializeField] private StackManager ammoStackManager;

        #endregion

        #region Private Variables

        private int _maxStackCount;

        #endregion

        #endregion

        private void Start()
        {
            _maxStackCount = manager.SendDataToControllers();
        }

        private void OnTriggerEnter(Collider other)
        {
            var gO = other.transform.parent.gameObject;
            if (other.CompareTag("MineStack"))
            {
                StackSignals.Instance.onClearStaticStack?.Invoke(transform);
            }
            else if (other.CompareTag("Hostage"))
            {
                StackSignals.Instance.onAddHostageToStack?.Invoke(gO);
                BaseSignals.Instance.onPlaceNewHostage?.Invoke(other.transform);
            }
            else if(other.CompareTag("Money"))
            {
                StackSignals.Instance.onAddStack?.Invoke(transform.parent.GetInstanceID(),gO);
                ScoreSignals.Instance.onUpdateMoneyScore?.Invoke(10);
            }
            else if (other.CompareTag("AmmoDepot"))
            {
                StackSignals.Instance.onAddAmmoBoxToPlayer?.Invoke(transform.parent.GetInstanceID());
            }
            else if (other.CompareTag("AmmoStack"))
            {
                var index = other.transform.GetSiblingIndex();
                var parent = other.transform.parent;
                StackSignals.Instance.onTransferBetweenStacks?.Invoke(parent.GetChild(index+1).GetComponent<StackManager>().GetInstanceID(),ammoStackManager,
                    parent.GetChild(index+1).GetComponent<StackManager>());
                StackSignals.Instance.onDeliverAmmoBox?.Invoke(parent.GetChild(index+1).GetComponent<StackManager>().transform.GetInstanceID(),_maxStackCount);
            }
            else if (other.CompareTag("Gate") && moneyStackManager.transform.childCount > 0)
            {
                StackSignals.Instance.onClearDynamicStack?.Invoke(manager.transform.GetInstanceID());
            }
            else if(other.CompareTag("Turret"))
            {
                manager.PlayerAtTurret(other.transform);
                BaseSignals.Instance.onSetPlayerToTurretShooter?.Invoke(other.transform.parent.GetInstanceID());
                CoreGameSignals.Instance.onSetCameraState?.Invoke(CameraStates.TurretCam);
                CoreGameSignals.Instance.onSetCameraAtTurret?.Invoke(transform);
            }
            else if (other.CompareTag("Shop"))
            {
                UISignals.Instance.onOpenShopPanel?.Invoke();
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
            else if (other.CompareTag("Shop"))
            {
                UISignals.Instance.onCloseShopPanel?.Invoke();
            }
        }
    }
}