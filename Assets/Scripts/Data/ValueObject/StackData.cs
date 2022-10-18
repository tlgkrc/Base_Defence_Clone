using System;
using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class StackData
    {
        public SerializedDictionary<StackTypes, StackGOData> StackDatas;
    }
}