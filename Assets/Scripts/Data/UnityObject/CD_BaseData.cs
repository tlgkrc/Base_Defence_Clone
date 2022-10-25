using Data.ValueObject.Base;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_BaseData", menuName = "Game/CD_BaseData", order = 0)]
    public class CD_BaseData : ScriptableObject
    {
        public BaseData BaseData;
    }
}