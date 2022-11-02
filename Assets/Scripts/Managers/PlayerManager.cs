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
        
        #endregion

        #region Private Variables
        
        private Rigidbody _rb;
        private WeaponData _weaponData;
        [ShowInInspector]private WeaponTypes _weaponTypes;
        private int _health;
        private bool _inBase;

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
            _health = Data.Health;
        }

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
            playerWeaponController.SetWeaponData(_weaponData);
            playerPhysicsController.SetPhysicData(Data.MaxStackCount);
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
            if (inBase)
            {
                var playerLayer = LayerMask.NameToLayer("Player");
                playerPhysicsController.gameObject.layer = playerLayer;
                playerWeaponController.enabled = false;
                playerWeaponController.SetWeaponVisual(false);
                animationController.SetWeaponAnimVisual(true);
                movementController.SetDangerZoneRotation(false);
            }
            else
            {
                var playerLayer = LayerMask.NameToLayer("DangerZone");
                playerPhysicsController.gameObject.layer = playerLayer;
                 playerWeaponController.enabled = true;
                playerWeaponController.SetWeaponVisual(true);
                animationController.SetWeaponAnimVisual(false);
                movementController.SetDangerZoneRotation(true);
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

        public void CheckFootAnim(bool isForward)
        {
            animationController.SetFootAnim(isForward);
        }

        private void OnUpdatePlayerHealth(int damage)
        {
            CoreGameSignals.Instance.onSetPlayerHealthRatio?.Invoke((float)_health/Data.Health);
            _health -= damage;
            Debug.Log(_health);
            if (_health<=0)
            {
                movementController.DisableMovement();
                animationController.SetFootAnim(true);
                animationController.SetAnimState(PlayerAnimStates.Die);
                ResetPlayer();
                BaseSignals.Instance.onSetEnemyTarget?.Invoke();
            }
        }

        async void ResetPlayer()
        {
            while (_inBase ==false && _health<Data.Health)
            {
                _health += 5;
                Debug.Log(_health);
                await Task.Delay(3000);
                animationController.SetFootAnim(true);
                SetPlayerLayer(true);
                gameObject.transform.position = new Vector3(0,0,10);
                _inBase = true;
            }
        }

        private Transform OnGetPlayerTransform()
        {
            return transform;
        }
    }
}
