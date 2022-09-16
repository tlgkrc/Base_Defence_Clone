using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.ValueObject
{
    [Serializable]
    public class StackData
    {
        public float CollectableOffsetInStack = 1;

        [Header("Lerp Speed")]
        [Range(0,1f)]
        public float LerpSpeed_x = 0.25f;
        [Range(0,1f)]
        public float LerpSpeed_y = 0.25f;
        [Range(0,1f)]
        public float LerpSpeed_z = 0.25f;
        
        [Range(0, 0.2f)] 
        public float ShackAnimDuraction = 0.12f;
        [Range(1f,3f)] 
        public float ShackScaleValue = 1f;

        [Range(1,20)]
        public int InitialStackItem = 5;
        
        public float DistanceFormPlayer = -1f;
    }
}