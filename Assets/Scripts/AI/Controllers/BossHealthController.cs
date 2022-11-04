using AI.Subscribers;
using Data.ValueObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AI.Controllers
{
    public class BossHealthController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Boss manager;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Image healthImage;

        #endregion

        #region Private Variables

        private int _health;
        private BossData _bossData;

        #endregion

        #endregion

        private void Start()
        {
            SetHealthText();
            healthImage.fillAmount = 1;
        }

        public void SetHealthData(BossData bossData)
        {
            _bossData = bossData;
            _health = _bossData.Health;
        }

        public void Hit(int damage)
        {
            _health -= damage;
            healthImage.fillAmount = (float)_health / _bossData.Health;
            SetHealthText();
            if (_health<=0)
            {
                manager.OpenPortal();
            }
        }

        private void SetHealthText()
        {
            healthText.text = (_health * 10).ToString();
        }
    }
}