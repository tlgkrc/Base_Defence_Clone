using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_BossData", menuName = "Game/CD_BossData", order = 0)]
    public class CD_BossData : ScriptableObject
    {
        public BossData BossData;
    }
}