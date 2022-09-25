using System;
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
        
        [SerializeField]private CinemachineVirtualCamera levelCamera;

        #endregion

        #region Private Variables
        
        private Vector3 _initialPosition;
        private CameraStates _cameraStateValue = CameraStates.LevelCam;
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

        }

        private void UnsubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void SetCameraStates()
        {
            if (CameraStateController == CameraStates.LevelCam)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
        }

        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            levelCamera.Follow = playerManager;
        }
     
        private void OnReset()
        {
            CameraStateController = CameraStates.LevelCam;
            levelCamera.Follow = null; 
            levelCamera.LookAt = null;
            levelCamera = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        }
    }
}