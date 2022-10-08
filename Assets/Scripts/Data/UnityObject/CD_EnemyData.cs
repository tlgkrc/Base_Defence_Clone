using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_EnemyData", menuName = "Game/CD_EnemyData", order = 0)]
    public class CD_EnemyData : ScriptableObject
    {
        public EnemyDatas EnemyDatas;
    }
}