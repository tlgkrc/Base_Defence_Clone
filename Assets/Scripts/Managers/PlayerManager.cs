using System.Collections;
using System.Threading.Tasks;
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
        [SerializeField] private PlayerWeaponController playerWeaponController;
        [SerializeField] private PlayerHealthController healthController;
        
        #endregion

        #region Private Variables
        
        private Rigidbody _rb;
        private WeaponData _weaponData;
        [ShowInInspector]private WeaponTypes _weaponTypes;
        private bool _inBase;

        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
            SendPlayerDataToControllers();
            playerWeaponController.enabled = false;
            _inBase = true;
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
            playerWeaponController.SetWeaponData(_weaponData);
            playerPhysicsController.SetPhysicData(Data.MaxStackCount);
            healthController.SetHealthData(Data);
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
            CoreGameSignals.Instance.onCheckCloseEnemy += OnCheckCloseEnemy;
            CoreGameSignals.Instance.onDieEnemy += OnDieEnemy;
            CoreGameSignals.Instance.onUpdatePlayerHealth += OnUpdatePlayerHealth;
            CoreGameSignals.Instance.onGetPlayerTransform += OnGetPlayerTransform;
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
            UISignals.Instance.onHoldWeapon -= OnHoldWeapon;
            CoreGameSignals.Instance.onCheckCloseEnemy -= OnCheckCloseEnemy;
            CoreGameSignals.Instance.onDieEnemy -= OnDieEnemy;
            CoreGameSignals.Instance.onUpdatePlayerHealth -= OnUpdatePlayerHealth;
            CoreGameSignals.Instance.onGetPlayerTransform -= OnGetPlayerTransform;

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
            _inBase = inBase;
            if (_inBase)
            {
                var playerLayer = LayerMask.NameToLayer("Player");
                playerPhysicsController.gameObject.layer = playerLayer;
                playerWeaponController.enabled = false;
                playerWeaponController.SetWeaponVisual(false);
                animationController.SetWeaponAnimVisual(true);
                movementController.SetDangerZoneRotation(false);
                StartCoroutine(healthController.FixedHealth());
            }
            else
            {
                var playerLayer = LayerMask.NameToLayer("DangerZone");
                playerPhysicsController.gameObject.layer = playerLayer;
                 playerWeaponController.enabled = true;
                playerWeaponController.SetWeaponVisual(true);
                animationController.SetWeaponAnimVisual(false);
                movementController.SetDangerZoneRotation(true);
                UISignals.Instance.onSetPlayerHealthPanel?.Invoke(true);
                StopAllCoroutines();
            }
        }

        private int OnGetMaxPlayerStackCount()
        {
            return Data.MaxStackCount;
        }

        private void OnHoldWeapon(WeaponTypes weaponType)
        {
            playerWeaponController.TakeHandWeapon(weaponType);
            animationController.SetWeaponAnimState(weaponType);
        }

        private void OnCheckCloseEnemy()
        {
            movementController.UpdateDangerZoneTarget(playerWeaponController.CheckTarget());
        }

        private void OnDieEnemy(GameObject gO)
        {
            playerWeaponController.EnemyDie(gO);
        }

        private void OnUpdatePlayerHealth(int damage)
        {
            healthController.UpdatePlayerHealth(damage);
            if (healthController.CheckHealth()<=0)
            {
                animationController.SetWeaponAnimVisual(true);
                playerWeaponController.SetWeaponVisual(false);
                playerWeaponController.enabled = false;
                ResetPlayer();
                BaseSignals.Instance.onSetEnemyTarget?.Invoke();
            }
        }

        async void ResetPlayer()
        {
            animationController.SetAnimState(PlayerAnimStates.Die);
            await Task.Delay(3000);
            transform.rotation = Quaternion.Euler(Vector3.zero);
            gameObject.transform.position = new Vector3(0,0,10);
            SetPlayerLayer(true);
        }

        private Transform OnGetPlayerTransform()
        {
            return transform;
        }

        
    }
}
