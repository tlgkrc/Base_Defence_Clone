using System.Collections.Generic;
using Data.ValueObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_StackData", menuName = "Game/CD_StackData", order = 0)]
    public class CD_StackData : SerializedScriptableObject
    {
        public StackData Datas;
    }
}