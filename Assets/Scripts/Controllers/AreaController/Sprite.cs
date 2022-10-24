using Signals;
using UnityEngine;

namespace Controllers.AreaController
{
    
    public class Sprite : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private int speed;

        #endregion

        #region Private Variables

        private Material _material;
        private static readonly int Arc2 = Shader.PropertyToID("_Arc2");
        private int _currenValue;
        private float _value = 0;

        #endregion

        #endregion

        private void Awake()
        {
            _material = spriteRenderer.material;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _value += Time.time * speed;
                _currenValue = (int)(360 - _value);
                if (_currenValue > 0)
                {
                    _material.SetFloat(Arc2, _currenValue);
                }
                else
                {
                    SendBaseSignals(tag);
                    transform.parent.gameObject.SetActive(false);
                }
            }
        }

        private void SendBaseSignals(string sectionTag)
        {
            switch (sectionTag)
            {
                case "StageArea":
                    BaseSignals.Instance.onUpdateStageArea?.Invoke(transform.parent.transform.parent.gameObject);
                    break;
                case "StageTurret":
                    break;
                case "Weapon":
                    break;
                case "TurretShooter" :
                    BaseSignals.Instance.onOpenTurretWorker?.Invoke();
                    break;
                default:
                    Debug.Log("You forgot a section!!!");
                    break;
            }
        }
    }
}