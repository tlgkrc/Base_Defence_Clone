using UnityEngine;
using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject;
using Data.ValueObject.Weapon;
using Enums;
using Enums.Animations;
using Keys;
using Signals;
using Sirenix.OdinInspector;

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
        [SerializeField] private PlayerGunController playerGunController;
        [SerializeField] private PlayerAttackController playerAttackController;
        
        #endregion

        #region Private Variables
        
        private Rigidbody _rb;
        private WeaponData _weaponData;
        [ShowInInspector]private WeaponTypes _weaponTypes;
        
        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;
        private WeaponData GetGunData() => Resources.Load<CD_Gun>("Data/CD_Gun").weaponData;

        private void GetReferences()
        {
            Data = GetPlayerData();
            _weaponData = GetGunData();
        }

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
            playerGunController.SetGunData(_weaponData);
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
            BaseSignals.Instance.onPlayerInBase += OnPlayerInBase;
            BaseSignals.Instance.onSetPlayerTransformAtTurret += OnSetPlayerTransformAtTurret;
            StackSignals.Instance.onGetMaxPlayerStackCount += OnGetMaxPlayerStackCount;
            UISignals.Instance.onHoldWeapon += OnHoldWeapon;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactivateMovement;
            InputSignals.Instance.onJoystickDragged -= OnSetIdleInputValues;
            CoreGameSignals.Instance.onReset -= OnReset;
            BaseSignals.Instance.onPlayerInBase -= OnPlayerInBase;
            BaseSignals.Instance.onSetPlayerTransformAtTurret -= OnSetPlayerTransformAtTurret;
            StackSignals.Instance.onGetMaxPlayerStackCount -= OnGetMaxPlayerStackCount;
            UISignals.Instance.onHoldWeapon += OnHoldWeapon;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnActivateMovement()
        {
            SetStackPosition();
            movementController.EnableMovement();
            animationController.SetAnimState(PlayerAnimStates.Walk);
            StackSignals.Instance.onActivateMoveHostageStack?.Invoke(true);
        }

        private void OnDeactivateMovement()
        {
            movementController.DisableMovement();
            animationController.SetAnimState(PlayerAnimStates.Idle); 
            animationController.ResetAnimSpeed();
            StackSignals.Instance.onActivateMoveHostageStack?.Invoke(false);
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
        
        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }
        private void OnReset()
        {
            gameObject.SetActive(true);
            movementController.OnReset();
        }
        
        

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
                playerAttackController.enabled = false;
                playerGunController.SetWeaponVisual(false);
                animationController.SetWeaponAnimVisual(true);
            }
            else
            {
                var playerLayer = LayerMask.NameToLayer("DangerZone");
                playerPhysicsController.gameObject.layer = playerLayer;
                playerAttackController.enabled = true;
                playerGunController.SetWeaponVisual(true);
                animationController.SetWeaponAnimVisual(false);
            }
        }

        private int OnGetMaxPlayerStackCount()
        {
            return Data.MaxStackCount;
        }

        public int SendDataToControllers()
        {
            return Data.MaxStackCount;
        }

        private void OnHoldWeapon(WeaponTypes weaponType)
        {
            playerGunController.TakeHandWeapon(weaponType);
            animationController.SetWeaponAnimState(weaponType);
        }
    }
}
