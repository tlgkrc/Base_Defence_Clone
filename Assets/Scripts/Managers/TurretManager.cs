using System;
using System.Collections.Generic;
using Keys;
using Signals;
using UnityEngine;

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
        
        #endregion

        #region Private Variables

        private bool _hasShooter;
        private bool _shooterIsPlayer;
        private List<GameObject> _hitList = new List<GameObject>();
        private const string Bullet = "Bullet";
        private int _bulletCount;

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
            BaseSignals.Instance.onSetTurretShooter += OnSetTurretShooter;
            BaseSignals.Instance.onReleasePlayer += OnReleasePlayer;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onSetTurretShooter -= OnSetTurretShooter;
            BaseSignals.Instance.onReleasePlayer -= OnReleasePlayer;
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
        
        private void OnSetTurretShooter(GameObject gameObject , bool isPlayer)
        {
            _hasShooter = true;
            _shooterIsPlayer = isPlayer;
        }

        private void OnReleasePlayer()
        {
            _hasShooter = false;
        }

        private void Shoot()
        {
            _bulletCount = stackManager.transform.childCount*4;
            if (_bulletCount<=0)
            {
                return;
            }
            var gO = PoolSignals.Instance.onGetPoolObject(Bullet, muzzle);
            
            SetTurretRotate();

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

        public void AddEnemyToHitList(GameObject gameObject)
        {
            _hitList.Add(gameObject);
        }
        
    }
}