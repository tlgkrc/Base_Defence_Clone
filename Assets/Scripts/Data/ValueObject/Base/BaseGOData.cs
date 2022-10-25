using System;
using Enums.Animations;
using UnityEngine.Rendering;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class BaseGOData
    {
        public MineData MineData;
        public SerializedDictionary<RoomNames, RoomData> RoomsData;
    }
}