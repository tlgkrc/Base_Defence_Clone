using System;
using Enums;
using Enums.Animations;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        private static readonly int Speed = Animator.StringToHash("Speed");
        #endregion

        #endregion

        public void SetAnimState(PlayerAnimStates animState)
        {
            animator.SetTrigger(animState.ToString());
        }
        
        public void SetWeaponAnimState(WeaponTypes weaponType)
        {
            animator.SetTrigger(weaponType.ToString());
        }

        public void SetSpeedVariable(IdleInputParams inputParams)
        {
            var animVector = new Vector2(inputParams.ValueX, inputParams.ValueZ);
            var animSpeed = animVector.magnitude;
            animator.speed = animSpeed;
        }

        public void ResetAnimSpeed()
        {
            animator.speed = 1;
        }

        public void SetWeaponAnimVisual(bool inBase)
        {
            if (inBase)
            {
                animator.SetLayerWeight(1, 0);
            }
            else
            {
                animator.SetLayerWeight(1, 1);
            }
        }
    }
}