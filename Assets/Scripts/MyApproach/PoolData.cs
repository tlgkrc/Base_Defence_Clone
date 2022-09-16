using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MyApproach
{
    [Serializable]
    public class PoolData
    {
        public MonoBehaviour PoolGameObject;
        [Range(1,100)]
        public int minNumber;
        [Range(100,500)]
        public int maxNumber;
        public Transform insTransform;
    }
}