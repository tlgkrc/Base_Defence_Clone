using Cinemachine;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField]private CinemachineVirtualCamera levelCamera;
        [SerializeField] private CinemachineVirtualCamera turretCamera;
        [SerializeField] private CinemachineVirtualCamera finishCamera;

        #endregion

        #region Private Variables
        
        private Vector3 _initialPosition;
        private CameraStates _cameraState = CameraStates.LevelCam;
        private Animator _camAnimator;
        
        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void Start()
        {
            OnSetCameraTarget();
        }

        private void GetReferences()
        {
            levelCamera = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
            _camAnimator = GetComponent<Animator>();
            _initialPosition = levelCamera.transform.localPosition;
        }

        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onSetCameraState += OnSetCameraState;
            CoreGameSignals.Instance.onSetCameraAtTurret += OnSetCameraAtTurret;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onSetCameraState -= OnSetCameraState;
            CoreGameSignals.Instance.onSetCameraAtTurret -= OnSetCameraAtTurret;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void SetCameraStates()
        {
            if (_cameraState == CameraStates.LevelCam)
            {
                _camAnimator.Play(CameraStates.LevelCam.ToString());
            }
            else if (_cameraState == CameraStates.TurretCam)
            {
                _camAnimator.Play(CameraStates.TurretCam.ToString());
            }
            else if (_cameraState == CameraStates.FinishCam)
            {
                _camAnimator.Play(CameraStates.FinishCam.ToString());
            }
        }

        private void OnSetCameraState(CameraStates cameraState)
        {
            _cameraState = cameraState;
            SetCameraStates();
        }
        
        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            levelCamera.Follow = playerManager;
        }
     
        private void OnReset()
        {
            _cameraState = CameraStates.LevelCam;
            levelCamera.Follow = null; 
            levelCamera.LookAt = null;
            levelCamera = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        }

        private void OnSetCameraAtTurret(Transform playerTransform)
        {
            var newVec = playerTransform.position + Vector3.back * 5f + Vector3.up * 4f;
            turretCamera.transform.position = newVec;
        }
    }
}