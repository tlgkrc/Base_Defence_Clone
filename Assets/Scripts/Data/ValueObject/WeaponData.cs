using System;
using Enums;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class WeaponData
    {
        public SerializedDictionary<WeaponTypes, WeaponGOData> WeaponDatas;
    }
}