using System;
using System.Collections;
using DG.Tweening;
using Enums;
using Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class GrenadeController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private new Collider collider;
        [SerializeField] private new ParticleSystem particleSystem;
        [SerializeField] private SpriteRenderer spriteRenderer;

        #endregion

        #region Private Variables



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
        }

        #region Event Supscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onSetThrowingStar += OnSetThrowingStar;
            BaseSignals.Instance.onSetThrowForce += OnSetThrowForce;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onSetThrowingStar -= OnSetThrowingStar;
            BaseSignals.Instance.onSetThrowForce -= OnSetThrowForce;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (CompareTag("Ground"))
            {
                Explosion();
                //StartCoroutine(StopExplosion());
            }
        }

        private void OnSetThrowingStar(Vector3 position)
        {
            
        }

        private void OnSetThrowForce(Vector3 velocity)
        {
            Throw(velocity);
        }

        private void Throw(Vector3 velocity)
        {
            rigidbody.isKinematic = false;
            rigidbody.freezeRotation = false;
            collider.enabled = true;
            rigidbody.AddForce(velocity,ForceMode.VelocityChange);
        }
        
        private void Explosion()
        {
            particleSystem.Play(true);

        }

        private IEnumerator StopExplosion()
        {
            yield return new WaitForSeconds(3f);
            particleSystem.Stop(true);
            ResetGrenade();
            PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.Grenade.ToString(), gameObject);
        }
    }
}