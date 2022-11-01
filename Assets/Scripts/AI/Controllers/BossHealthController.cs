using AI.Subscribers;
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

        #endregion

        #endregion

        public void SetHealthData(int health)
        {
            _health = health;
        }

        public void Hit(int damage)
        {
            _health -= damage;
        }
    }
}