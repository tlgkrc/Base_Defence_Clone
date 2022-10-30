using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers.Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerAttackController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables
        
        
        #endregion

        #region Private Variables

        [ShowInInspector] private List<GameObject> _enemies = new List<GameObject>();

        #endregion
        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemies.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                _enemies.Remove(other.gameObject);
                _enemies.TrimExcess();
            }
        }

        private void Update()
        {
            if (_enemies.Count<=0)
            {
                return;
            }
            Shoot();
        }

        private void Shoot()
        {
            
        }


        // private void AutoShoot()
        // {
        //     _shootTime += Time.deltaTime;
        //     while (_shootTime >= _turretGOData.ShootingDelay)
        //     {
        //         _shootTime = 0;
        //         _shootingCount += 1;
        //         _bulletCount--;
        //         var gO = PoolSignals.Instance.onGetPoolObject(PoolTypes.Bullet.ToString(), muzzle);
        //         gO.transform.localRotation = transform.rotation;
        //         if (_shootingCount == 4)
        //         {
        //             StackSignals.Instance.onRemoveLastElement?.Invoke(stackManager.transform.GetInstanceID(),PoolTypes.BulletBox.ToString());
        //             _shootingCount = 0;
        //         }
        //     }
        // }
    }
}