using Cinemachine;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        public CameraStates CameraStateController
        {
            get => _cameraStateValue;
            set
            {
                _cameraStateValue = value;
                SetCameraStates();
            }
        }
        
        #endregion
        #region Serialized Variables
        
        [SerializeField]private CinemachineVirtualCamera runnerCamera;
        [SerializeField]private CinemachineVirtualCamera idleStartCamera;
        [SerializeField]private CinemachineVirtualCamera idleCamera;

        #endregion

        #region Private Variables
        
        private Vector3 _initialPosition;
        private CameraStates _cameraStateValue = CameraStates.InitializeCam;
        private Animator _camAnimator;
        
        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            GetInitialPosition();
        }

        private void GetReferences()
        {
            runnerCamera = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
            idleStartCamera = transform.GetChild(2).GetComponent<CinemachineVirtualCamera>();
            idleCamera = transform.GetChild(3).GetComponent<CinemachineVirtualCamera>();
            _camAnimator = GetComponent<Animator>();
        }
        
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameStateToIdle;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameStateToIdle;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void SetCameraStates()
        {
            if (CameraStateController == CameraStates.InitializeCam)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
            else if (CameraStateController == CameraStates.RunnerCam)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
            else if (CameraStateController == CameraStates.IdleStartCam)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
            else if (CameraStateController == CameraStates.IdleCam)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
        }
        
        private void GetInitialPosition()
        {
            _initialPosition = runnerCamera.transform.localPosition;
        }

        private void OnMoveToInitialPosition()
        {
            runnerCamera.transform.localPosition = _initialPosition;
        }

        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            runnerCamera.Follow = playerManager;
            idleCamera.Follow = playerManager;
            idleStartCamera.Follow = playerManager;
            CameraStateController = CameraStates.RunnerCam;
        }
        
        private void OnNextLevel()
        {
            CameraStateController = CameraStates.InitializeCam;
        }
        private void OnChangeGameStateToIdle()
        {
            CameraStateController = CameraStates.IdleCam;
        }
        private void OnLevelSuccessful()
        {
            CameraStateController = CameraStates.IdleStartCam;
        }

 

        private void OnReset()
        {
            CameraStateController = CameraStates.InitializeCam;
            runnerCamera.Follow = null; //referanceı state driven yap
            runnerCamera.LookAt = null;
            runnerCamera = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
            OnMoveToInitialPosition();
        }
    }
}