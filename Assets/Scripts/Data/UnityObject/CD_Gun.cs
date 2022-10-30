using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Gun", menuName = "Game/CD_Gun", order = 0)]
    public class CD_Gun : ScriptableObject
    {
        public WeaponData weaponData;
    }
}