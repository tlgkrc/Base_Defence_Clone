using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_SpawnData", menuName = "Game/CD_SpawnData", order = 0)]
    public class CD_SpawnData : ScriptableObject
    {
        public SpawnData SpawnData;
    }
}