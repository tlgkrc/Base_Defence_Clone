using System;
using System.Collections;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class GrenadeController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private new Collider collider;
        [SerializeField] private GameObject explosion;

        #endregion

        #region Private Variables

        private GameObject _explosionEffect;
        private float _radius = 2;
        private float _explosionForce = 700;
        private int _grenadeDamage = 4;

        #endregion

        #endregion

        private void Awake()
        {
            ResetGrenade();
        }

        private void ResetGrenade()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.isKinematic = true;
            rigidbody.freezeRotation = true;
            collider.enabled = false;
            explosion.SetActive(false);
        }

        #region Event Supscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onSetThrowForce += OnSetThrowForce;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onSetThrowForce -= OnSetThrowForce;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                explosion.SetActive(true);
                BaseSignals.Instance.onFinishExplosion?.Invoke();
                Explode();
                Invoke(nameof(StopExplosion),1.2f);
            }
        }

        private void OnSetThrowForce(Vector3 velocity,int id)
        {
            if (gameObject.GetInstanceID() == id)
            {
                Throw(velocity);
            }
        }

        private void Throw(Vector3 velocity)
        {

            rigidbody.isKinematic = false;
            rigidbody.freezeRotation = false;
            collider.enabled = true;
            rigidbody.AddForce(velocity,ForceMode.VelocityChange);
        }

        private void StopExplosion()
        {
            explosion.SetActive(false);
            ResetGrenade();
            PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.Grenade.ToString(), gameObject);
        }

        private void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

             foreach (var nearbyObject in colliders)
             {
                 if (nearbyObject.CompareTag("Player"))
                 {
                     CoreGameSignals.Instance.onUpdatePlayerHealth?.Invoke(_grenadeDamage);
                 }
             }
        }
    }
}