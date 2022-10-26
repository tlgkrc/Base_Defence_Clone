using System;
using Enums.Animations;
using UnityEngine.Rendering;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class TurretData
    {
        public SerializedDictionary<int, TurretGOData> TurretsData;
    }
}