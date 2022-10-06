using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class StackGOData
    {
        public bool IsDynamic;
        [HorizontalGroup("Level")]
        public float LevelOffset;
        [HorizontalGroup("Level")] 
        public float StartHeight;
        [HorizontalGroup("Grid")]
        public int GridX;
        [HorizontalGroup("Grid")] 
        public int GridZ;
        public int maxCount;
        [ShowIf("IsDynamic", true)] public Transform StockPile;
    }
}