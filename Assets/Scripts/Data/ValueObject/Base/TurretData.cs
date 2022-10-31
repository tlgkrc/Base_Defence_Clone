using System;
using Enums.Animations;
using Interfaces;
using UnityEngine.Rendering;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class TurretData 
    {
        public SerializedDictionary<int, TurretGOData> TurretsData;
    }
}