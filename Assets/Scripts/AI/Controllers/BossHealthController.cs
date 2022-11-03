using AI.Subscribers;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace AI.Controllers
{
    public class BossHealthController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private Boss manager;

        #endregion

        #region Private Variables

        private int _health;
        private BossData _bossData;

        #endregion

        #endregion

        public void SetHealthData(BossData bossData)
        {
            _bossData = bossData;
            _health = _bossData.Health;
        }

        public void Hit(int damage)
        {
            _health -= damage;
            if (_health<=0)
            {
                manager.OpenPortal();
            }
            CoreGameSignals.Instance.onSetBossHealthRatio?.Invoke((float)_health/_bossData.Health);
            
        }
        
        
    }
}