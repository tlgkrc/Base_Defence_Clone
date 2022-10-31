using Data.ValueObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_BulletData", menuName = "Game/CD_BulletData", order = 0)]
    public class CD_BulletData : SerializedScriptableObject
    {
        public BulletData BulletData;
    }
}