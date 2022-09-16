using UnityEngine;
using DG.Tweening;
using Signals;
using System.Collections;

namespace Controllers
{
    public class TurretController: MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private float rotationSpeed = 1f, rotationAngle = 60;

        [SerializeField] private Transform taret1, taret2;

        [SerializeField] private ParticleSystem taret1Particle, taret2Particle;

        #endregion

        #endregion

        private void Start()
        {
            StartCoroutine(SearchAnim());
        }
        public void RotateToPlayer(Transform player)
        {
            StopAllCoroutines();
            var position = player.position;
            taret1.DOLookAt(position, rotationSpeed);
            taret2.DOLookAt(position, rotationSpeed);
            taret1Particle.Play();
            taret2Particle.Play();
        }

        public void OnTargetDisappear()
        {
            StartCoroutine(SearchAnim());

        }

        public IEnumerator SearchAnim()
        {
            yield return new WaitForSeconds(1f);
            rotationAngle *= -1;
            taret1.DORotate(new Vector3(0, rotationAngle, 0), 1f).SetEase(Ease.InOutBack);
            taret2.DORotate(new Vector3(0, rotationAngle * -1, 0), 1f).SetEase(Ease.InOutBack);
            StartCoroutine(SearchAnim());
        }
    }
}