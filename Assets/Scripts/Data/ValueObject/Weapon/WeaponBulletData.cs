using System;
using Enums;
using UnityEngine.Rendering;

namespace Data.ValueObject.Weapon
{
    [Serializable]
    public class WeaponBulletData
    {
        public SerializedDictionary<WeaponTypes, WeaponBulletGOData> WeaponBulletGODatas;
    }
}