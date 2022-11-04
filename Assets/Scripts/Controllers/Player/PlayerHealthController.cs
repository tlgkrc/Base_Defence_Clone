using System.Collections;
using Data.ValueObject;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerHealthController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;

        #endregion

        private int _health;
        private PlayerData _playerData;

        #endregion

        public void SetHealthData(PlayerData playerData)
        {
            _playerData = playerData;
            _health = playerData.Health;
        }

        public IEnumerator FixedHealth()
        {
            if (_health>= _playerData.Health)
            {
                UISignals.Instance.onSetPlayerHealthPanel?.Invoke(false);
            }
            if (_health < _playerData.Health) 
            {
                _health += 1;
                UISignals.Instance.onUpdatePlayerHealthBar((float)_health / _playerData.Health);
                yield return new WaitForSeconds(.1f);
                StartCoroutine(FixedHealth());
            }
            else
            {
                yield break;
            }
        }
        
        public void UpdatePlayerHealth(int damage)
        {
            _health -= damage;
            UISignals.Instance.onUpdatePlayerHealthBar((float)_health / _playerData.Health);
        }

        public int CheckHealth()
        {
            return _health;
        }
    }
}