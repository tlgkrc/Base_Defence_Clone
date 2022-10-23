using UnityEngine;
using Controllers;
using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Enums.Animations;
using Keys;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private Vector3 offset;

        #endregion

        #region Private Variables
        
        private Rigidbody _rb;
        
        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        private void GetReferences()
        {
            Data = GetPlayerData();
        }

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
        }

        #region Event Subscription

        private void OnEnable()
        {
            movementController.IsReadyToPlay(true);
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactivateMovement;
            InputSignals.Instance.onJoystickDragged += OnSetIdleInputValues;
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            BaseSignals.Instance.onPlayerInBase += OnPlayerInBase;
            BaseSignals.Instance.onSetPlayerTransformAtTurret += OnSetPlayerTransformAtTurret;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactivateMovement;
            InputSignals.Instance.onJoystickDragged -= OnSetIdleInputValues;
            CoreGameSignals.Instance.onReset -= OnReset;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            BaseSignals.Instance.onPlayerInBase -= OnPlayerInBase;
            BaseSignals.Instance.onSetPlayerTransformAtTurret -= OnSetPlayerTransformAtTurret;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Event Methods

        #region Movement Controller

        private void OnActivateMovement()
        {
            SetStackPosition();
            movementController.EnableMovement();
            animationController.SetAnimState(PlayerAnimStates.Walk);
        }

        private void OnDeactivateMovement()
        {
            movementController.DisableMovement();
            animationController.SetAnimState(PlayerAnimStates.Idle); 
            animationController.ResetAnimSpeed();
        }

        private void OnSetIdleInputValues(IdleInputParams inputParams)
        {
            movementController.UpdateIdleInputValue(inputParams);
            animationController.SetSpeedVariable(inputParams);
        }

        private void OnPlayerInBase(bool inBase)
        {
            SetPlayerLayer(inBase);
        }

        private void OnSetPlayerTransformAtTurret(TurretTransformParams turretTransformParams)
        {
            movementController.UpdateTurretTransformParams(turretTransformParams);
        }

        #endregion

        #region Others

        private void OnLevelSuccessful()
        {
            
        }
        
        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }
        private void OnReset()
        {
            gameObject.SetActive(true);
            movementController.OnReset();
        }

        #endregion

        #endregion

        #region Methods

        private void SetStackPosition()
        {
            StackSignals.Instance.onPlayerGameObject?.Invoke(gameObject);
        }

        public void PlayerAtTurret(Transform turretTransform)
        {
            animationController.SetAnimState(PlayerAnimStates.Hold);
            movementController.DisableMovement();
            movementController.EnableTurretMovement(true);
        }

        public void ReleaseFromTurret()
        {
            animationController.SetAnimState(PlayerAnimStates.Walk);
            movementController.EnableMovement();
            movementController.EnableTurretMovement(false);
        }

        private void SetPlayerLayer(bool inBase)
        {
            if (inBase)
            {
                var playerLayer = LayerMask.NameToLayer("Player");
                playerPhysicsController.gameObject.layer = playerLayer;
            }
            else
            {
                var playerLayer = LayerMask.NameToLayer("DangerZone");
                playerPhysicsController.gameObject.layer = playerLayer;
            }
        }
        
        #endregion
    }
}
