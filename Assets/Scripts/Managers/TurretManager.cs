using System.Collections;
using System.Collections.Generic;
using Keys;
using Signals;
using UnityEngine;
using Controllers.Turret;
using Data.UnityObject;
using Data.ValueObject.Base;
using Enums;
using TMPro;

namespace Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private Transform muzzle;
        [SerializeField] private StackManager stackManager;
        [SerializeField] private Transform shooterTransform;
        [SerializeField] private GameObject turretWorker;
        [SerializeField] private TurretPhysicController physicController;
        [SerializeField] private int id;
        [SerializeField] private TextMeshPro costTurretAI;
        
        #endregion

        #region Private Variables

        private bool _hasShooter;
        private bool _shooterIsPlayer;
        private List<GameObject> _hitList = new List<GameObject>();
        private int _bulletCount;
        private float _shootTime;
        private int _shootingCount;
        private TurretGOData _turretGOData;
        private bool _isOnArea;
        private int _moneyToPay;

        #endregion
        #endregion

        private void Awake()
        {
            _hasShooter = false;
            _turretGOData = GetTurretData();
            GetReferences();
        }

        private TurretGOData GetTurretData()
        {
            return Resources.Load<CD_TurretData>("Data/CD_TurretData").TurretData.TurretsData[id];
        }

        private void GetReferences()
        {
            SetTurretCostText(_turretGOData.TurretSoldierCost);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onSetPlayerToTurretShooter += OnSetPlayerTurretShooter;
            BaseSignals.Instance.onReleasePlayer += OnReleasePlayer;
            BaseSignals.Instance.onOpenTurretWorker += OnOpenTurretWorker;
            StackSignals.Instance.onDeliverAmmoBox += OnCountAmmo;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onSetPlayerToTurretShooter -= OnSetPlayerTurretShooter;
            BaseSignals.Instance.onReleasePlayer -= OnReleasePlayer;
            BaseSignals.Instance.onOpenTurretWorker -= OnOpenTurretWorker;
            StackSignals.Instance.onDeliverAmmoBox -= OnCountAmmo;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            if (_hasShooter)
            {
                Shoot();
            }
        }
        
        private void OnSetPlayerTurretShooter(int id)
        {
            if (id == transform.GetInstanceID())
            {
                _hasShooter = true;
                _shooterIsPlayer = true;
            }
        }

        private void OnOpenTurretWorker()
        {
            _hasShooter = true;
            _shooterIsPlayer = false;
            turretWorker.SetActive(true);
            physicController.CloseCollider();
            physicController.transform.parent.gameObject.SetActive(false);
        }

        private void OnReleasePlayer()
        {
            _hasShooter = false;
            _shooterIsPlayer = false;
        }

        private void OnCountAmmo(int id,int amount)
        {
            if (stackManager.transform.GetInstanceID() == id)
            {
                _bulletCount += amount*4;
            }
        }

        private void Shoot()
        {
            SetTurretRotate();
            if (_bulletCount<=0)
            {
                return;
            }
            AutoShoot();
        }

        private void SetTurretRotate()
        {
            if (_shooterIsPlayer)
            {
                var rotationX = BaseSignals.Instance.onSetTurretRotation?.Invoke();
                var newVec =_turretGOData.RotationBorderOffset * rotationX ;
                if (newVec == null) return;
                Transform transform1;
                (transform1 = transform).rotation = Quaternion.Euler((Vector3)newVec);
                BaseSignals.Instance.onSetPlayerTransformAtTurret?.Invoke(new TurretTransformParams()
                {
                    Position = shooterTransform.position,
                    Quaternion =  transform1.rotation
                });
            }
            else
            {
                if (_hitList.Count != 0)
                {
                    transform.LookAt(_hitList[0].transform.position);
                }
            }
        }

        public void AddEnemyToHitList(GameObject gO)
        {
            _hitList.Add(gO);
        }

        public void RemoveEnemyFromHitList(GameObject gO)
        {
            _hitList.Remove(gO);
            _hitList.TrimExcess();
        }

        private void AutoShoot()
        {
            _shootTime += Time.deltaTime;
            while (_shootTime >= _turretGOData.ShootingDelay)
            {
                _shootTime = 0;
                _shootingCount += 1;
                _bulletCount--;
                var gO = PoolSignals.Instance.onGetPoolObject(PoolTypes.Bullet.ToString(), muzzle);
                gO.transform.localRotation = transform.rotation;
                if (_shootingCount == 4)
                {
                   StackSignals.Instance.onRemoveLastElement?.Invoke(stackManager.transform.GetInstanceID(),PoolTypes.BulletBox.ToString());
                   _shootingCount = 0;
                }
            }
        }

        private void SetTurretCostText(int cost)
        {
            costTurretAI.text = cost.ToString();
        }

        public void BuyTurretWorker()
        {
            _isOnArea = true;
            StartCoroutine(Buy());
        }

        public void StopBuying()
        {
            StopCoroutine(Buy());
            _isOnArea = false;
        }
        
        private IEnumerator Buy()
        {
            while (_isOnArea)
            {
                _moneyToPay = _turretGOData.TurretSoldierCost - _turretGOData.TurretSoldierCostPaid;
                physicController.SetRadialVisual(_turretGOData.TurretSoldierCostPaid,_turretGOData.TurretSoldierCost);
                SetRoomMoney(_moneyToPay);
                if(_moneyToPay == 0)
                {
                    OnOpenTurretWorker();
                    yield break;
                }
                _turretGOData.TurretSoldierCostPaid++;
                yield return new WaitForSeconds(_turretGOData.BuyDelay);
            }
        }

        private void SetRoomMoney(int money)
        {
            costTurretAI.text = money.ToString();
        }
    }
}