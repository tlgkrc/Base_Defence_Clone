using Commands;
using UnityEngine;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
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

        #endregion

        #region Private Variables
        
        private Rigidbody _rb;

        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
            SetStackPosition();
            SendPlayerDataToControllers();
            animationController.SetAnimState(CollectableAnimStates.Idle);
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;
        
        private void Init()
        {
            var transform1 = transform;
            _rb = GetComponent<Rigidbody>();//???????
        }

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
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onJoystickDragged += OnSetIdleInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;

        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onJoystickDragged -= OnSetIdleInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
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
            movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
        }

        private void OnSetIdleInputValues(IdleInputParams inputParams)
        {
            movementController.UpdateIdleInputValue(inputParams);
            animationController.SetSpeedVariable(inputParams);
        }

        #endregion

        #region Others

        private void OnPlay()
        {
            SetStackPosition();
            movementController.IsReadyToPlay(true);
            animationController.SetAnimState(CollectableAnimStates.Run);
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);
            animationController.SetAnimState(CollectableAnimStates.Idle);
        }
        
        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }
        private void OnReset()
        {
            gameObject.SetActive(true);
            movementController.OnReset();
            SetStackPosition();
        }

        #endregion

        #endregion

        #region Methods

        private void SetStackPosition()
        {
            StackSignals.Instance.onPlayerGameObject?.Invoke(gameObject);
        }

        private void SetAnim(CollectableAnimStates animState)
        {
            animationController.SetAnimState(animState);
        }
        

        #endregion
    }
}
