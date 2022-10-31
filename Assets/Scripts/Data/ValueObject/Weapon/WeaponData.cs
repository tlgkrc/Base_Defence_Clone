using System;
using Enums;
using UnityEngine.Rendering;

namespace Data.ValueObject.Weapon
{
    [Serializable]
    public class WeaponData
    {
        public SerializedDictionary<WeaponTypes, WeaponGOData> WeaponDatas;
    }
}