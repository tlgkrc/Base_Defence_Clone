using System;
using AI.Controllers;
using Enums.Animations;
using UnityEngine;

namespace AI.Subscribers
{
    public class Boss : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BossPhysicController physicController;
        [SerializeField] private BossAnimController animController;
        [SerializeField] private BossHealthController healthController;
       

        #endregion

        #region Private Variables

        private GameObject _target;

        //Data//
        private int _health = 70;
        
        #endregion

        #endregion

        private void Awake()
        {
            SetDataToControllers();
        }

        private void SetDataToControllers()
        {
            healthController.SetHealthData(_health);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
            if (target == null)
            {
                animController.SetBossAnim(BossAnimTypes.Idle);
            }
            else
            {
                animController.SetBossAnim(BossAnimTypes.Throw);
            }
        }

        public void Hit(int damage)
        {
            healthController.Hit(damage);
        }

        public GameObject GetTarget()
        {
            return _target;
        }
    }
}