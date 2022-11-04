using AI.Subscribers;
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
        [SerializeField] private GameObject fakeGrenade;

        #endregion

        #region Private Variables

        private float _currentY;
        private float _currentXZ;
        private float _velocityXZ;
        private float _velocityY;
        private float _throwTime;
        private Vector3 _differenceXZ;
        private Vector3 _difference;
        private Vector3 _result;
        private GameObject _grenade;

        #endregion

        #endregion

        private void Awake()
        {
            fakeGrenade.SetActive(true);
        }

        public void SetThrowTime(float time)
        {
            _throwTime = time;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.SetTarget(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                manager.SetTarget(null);
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
            _velocityXZ = _currentXZ / _throwTime;
            _velocityY = _currentY / _throwTime + 0.5f * Mathf.Abs(Physics.gravity.y) * _throwTime;
            _result = _differenceXZ * _velocityXZ;
            _result.y = _velocityY;
            return _result;
        }

        public void ThrowBomb()
        {
            _grenade = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Grenade.ToString(),
                fakeGrenade.transform);
            BaseSignals.Instance.onSetThrowingStar?.Invoke(manager.GetTarget().transform.position);
            BaseSignals.Instance.onSetThrowForce?.Invoke(VelocityCalculate(),_grenade.GetInstanceID());
            fakeGrenade.SetActive(false);
        }

        public void PrepareThrow()
        {
            fakeGrenade.SetActive(true);
        }
    }
}