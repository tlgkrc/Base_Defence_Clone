using System;
using System.Collections;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class ThrowingStarController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private SpriteRenderer spriteRenderer;

        #endregion
        
        #region Private Variables


        #endregion

        #endregion

        private void Awake()
        { 
            ResetSprite();
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onSetThrowingStar += OnSetTrowingStar;
            BaseSignals.Instance.onFinishExplosion += OnFinishExplosion;
        }

        private void UnsubscribeEvents()
        {
            BaseSignals.Instance.onSetThrowingStar -= OnSetTrowingStar;
            BaseSignals.Instance.onFinishExplosion -= OnFinishExplosion;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void OnSetTrowingStar(Vector3 dangerZone)
        {
            transform.position = dangerZone;
            transform.DOScale(Vector3.one * 4, 0.2f);
            spriteRenderer.transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd);

        }

        private void OnFinishExplosion()
        {
            ResetSprite();
            StopAllCoroutines();
        }

        private void ResetSprite()
        {
            transform.localScale = Vector3.zero;
            transform.DOLocalRotate(Vector3.zero, .5F);
        }
    }
}