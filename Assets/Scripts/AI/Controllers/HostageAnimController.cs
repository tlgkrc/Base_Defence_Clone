using Enums.Animations;
using UnityEngine;

namespace AI.Controllers
{
    public class HostageAnimController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion

        #endregion

        public void SetAnim(HostageAnimState hostageAnimState)
        {
            animator.SetTrigger(hostageAnimState.ToString());
        }
    }
}