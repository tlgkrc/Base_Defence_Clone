using Data.ValueObject.Weapon;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_GrenadeData", menuName = "Game/CD_GrenadeData", order = 0)]
    public class CD_GrenadeData : ScriptableObject
    {
        public GrenadeData GrenadeData;
    }
}