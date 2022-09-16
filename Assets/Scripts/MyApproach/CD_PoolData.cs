using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MyApproach
{
    [CreateAssetMenu(fileName = "CD_PoolData", menuName = "Game/CD_PoolData", order = 0)]
    public class CD_PoolData : SerializedScriptableObject
    {
        public Dictionary<PoolTypes, PoolData> poolDatas = new Dictionary<PoolTypes, PoolData>();
    }
}