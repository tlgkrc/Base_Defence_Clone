using System;
using Enums;
using UnityEngine.Rendering;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class ShopData
    {
        public SerializedDictionary<WeaponTypes, ShopGOData> ShopGoDatas;
    } 
}