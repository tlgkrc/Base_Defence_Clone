using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Test
{
    public class PlayerTest : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables


        #endregion

        #region Private Variables

        private static Vector3 _lastPos = Vector3.back;

        #endregion

        #endregion

        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Money"))
            {
                Transform transform2;
                (transform2 = other.transform).SetParent(transform);
                //transform1.localPosition = _lastPos;
                var difVector3 = transform2.localPosition - _lastPos;
                var position = transform.position;
                Vector3 randomPoint = new Vector3(Random.Range(0, difVector3.x) + _lastPos.x,
                    Random.Range(0, difVector3.y) + position.y*2,
                    Random.Range(0, difVector3.z) + position.z*2);
                transform2.DOLocalMove(randomPoint, .3f).SetEase(Ease.InOutBack).OnComplete(() =>
                    transform2.DOLocalMove(_lastPos , .3f));
                transform2.DORotate(new Vector3(180,180,180), .3f).OnComplete(() => transform2.DORotate(Vector3.zero, .3f));
            }

            _lastPos += Vector3.up;
        }

        private void DoSomething()
        {
            
        }
    }
}