using System;
using System.Collections;
using AI.Subscribers;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace AI.Controllers
{
    public class BossAttackController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Boss manager;
        [SerializeField] private Transform grenadeTransform;
        [SerializeField] private float throwTime = 1.5f;

        #endregion

        #region Private Variables

        private GameObject _grenade;
        private float _currentY;
        private float _currentXZ;
        private float _velocityXZ;
        private float _velocityY;
        private Vector3 _differenceXZ;
        private Vector3 _difference;
        private Vector3 _result;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            manager.SetTarget(other.gameObject);
            StartCoroutine(Attack());
        }

        private void OnTriggerExit(Collider other)
        {
            manager.SetTarget(null);
            StopCoroutine(Attack());
        }
        
        public void PrepareBomb()
        {
            if (_grenade ==null)
            {
                _grenade = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Grenade.ToString(), grenadeTransform);
                _grenade.transform.parent = grenadeTransform;
                _grenade.transform.localPosition = Vector3.zero;
                _grenade.transform.DOLocalRotate(Vector3.zero, 0);
            }
        }

        private Vector3 VelocityCalculate()
        {
            _difference = manager.GetTarget().transform.position - _grenade.transform.position;
            _differenceXZ = _difference;
            _differenceXZ.Normalize();
            _differenceXZ.y = 0f;
            _currentY = _difference.y;
            _currentXZ = _difference.magnitude;
            _velocityXZ = _currentXZ / throwTime;
            _velocityY = _currentY / throwTime + 0.5f * Mathf.Abs(Physics.gravity.y) * throwTime;
            _result = _differenceXZ * _velocityXZ;
            _result.y = _velocityY;
            return _result;
        }

        public void ThrowBomb()
        {
            _grenade.transform.parent = BaseSignals.Instance.onSetBaseTransform?.Invoke();
            BaseSignals.Instance.onSetThrowingStar?.Invoke(manager.GetTarget().transform.position);
            BaseSignals.Instance.onSetThrowForce?.Invoke(VelocityCalculate());
            _grenade = null;
        }

        IEnumerator Attack()
        {
            while (manager.GetTarget() != null)
            {
                PrepareBomb();
                yield return new WaitForSeconds(3f);
                ThrowBomb();
            }
        }
    }
}