﻿using System;
using System.Collections.Generic;
using Keys;
using Signals;
using UnityEngine;
using System.Threading.Tasks;
using Controllers.Turret;
using Enums;

namespace Managers
{
    public class TurretManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private Transform muzzle;
        [SerializeField] private Transform barrel;
        [SerializeField] private StackManager stackManager;
        [SerializeField] private Transform shooterTransform;
        [SerializeField] private GameObject turretWorker;
        [SerializeField] private TurretPhysicController physicController;
        
        #endregion

        #region Private Variables

        private bool _hasShooter;
        private bool _shooterIsPlayer;
        private List<GameObject> _hitList = new List<GameObject>();
        private int _bulletCount;
        private float _shootTime;
        private int _shootingCount;

        #endregion
        #endregion

        private void Awake()
        {
            _hasShooter = false;
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
        
        private void OnSetPlayerTurretShooter()
        {
            _hasShooter = true;
            _shooterIsPlayer = true;
        }

        private void OnOpenTurretWorker()
        {
            _hasShooter = true;
            _shooterIsPlayer = false;
            turretWorker.SetActive(true);
            physicController.CloseCollider();
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
            Debug.Log(_bulletCount);
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
                var newVec =new Vector3(0, 45, 0) * rotationX ;
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
                transform.LookAt(_hitList[0].transform.position);
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
            while (_shootTime >= .2f)
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
    }
}