using System;
using System.Numerics;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData MovementData;
    }

    [Serializable]
    public class PlayerMovementData
    {
        public float ForwardSpeed;
        public float SidewaysSpeed;
        public float JumpDistance;
        public float JumpDuration;
        public float IdleRotateSpeed;
        public float RotateBorder;
        public float CrouchSpeed;
        public float RunSpeed;
    }
}