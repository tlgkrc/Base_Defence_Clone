using System;
using Sirenix.OdinInspector;

namespace Data.ValueObject
{
    [Serializable]
    public class HostageStackData
    {
        public float HostageOffsetInStack;
        [HorizontalGroup("Lerp")] public float LerpX;
        [HorizontalGroup("Lerp")] public float LerpZ;
    }
}