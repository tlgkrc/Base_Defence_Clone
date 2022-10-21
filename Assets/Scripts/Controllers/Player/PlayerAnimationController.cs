﻿using Enums;
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

        #endregion

        public void SetAnimState(PlayerAnimStates animState)
        {
            animator.SetTrigger(animState.ToString());
        }

        public void SetSpeedVariable(IdleInputParams inputParams)
        {
            float speedX = Mathf.Abs(inputParams.ValueX);
            float speedZ = Mathf.Abs(inputParams.ValueZ);
        }
    }
}