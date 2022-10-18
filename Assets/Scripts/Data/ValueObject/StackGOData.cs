using System;
using Enums;
using Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class StackGOData
    {
        public bool IsDynamic;
        [HorizontalGroup("Level")]
        public Vector3 Offset;
        [HorizontalGroup("Level")] 
        public float StartHeight;
        [HorizontalGroup("Grid")]
        public int Grid_1;
        [HorizontalGroup("Grid")] 
        public int Grid_2;
        public int MaxCount;
        public BaseAxis BaseAxis;
        public GameObject StackGameObject;
    }
}