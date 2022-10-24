using System;
using Enums;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class GunData
    {
        public SerializedDictionary<GunTypes, GunGOData> GunDatas;
    }
}