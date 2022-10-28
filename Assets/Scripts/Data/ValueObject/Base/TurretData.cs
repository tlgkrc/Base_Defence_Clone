using System;
using Enums.Animations;
using Interfaces;
using UnityEngine.Rendering;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class TurretData : ISaveableEntity
    {
        public SerializedDictionary<int, TurretGOData> TurretsData;
        public string GetKey()
        {
            throw new NotImplementedException();
        }
    }
}