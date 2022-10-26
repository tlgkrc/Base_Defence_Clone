using Data.ValueObject.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_BaseData", menuName = "Game/CD_BaseData", order = 0)]
    public class CD_BaseData : SerializedScriptableObject
    {
        public BaseData BaseData;
    }
}