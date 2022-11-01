using AI.Subscribers;
using Enums.Animations;
using UnityEngine;

namespace AI.Controllers
{
    public class BossAnimController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Boss manager;
        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        private float _shootingTimeSpan;

        #endregion

        #endregion

        public void SetAnimData(float shootingTimeSpan)
        {
            _shootingTimeSpan = shootingTimeSpan;
        }

        public void SetBossAnim(BossAnimTypes animType)
        {
            animator.SetTrigger(animType.ToString());
        }
        
    }
}