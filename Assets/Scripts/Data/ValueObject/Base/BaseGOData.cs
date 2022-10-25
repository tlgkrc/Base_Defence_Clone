using System;
using UnityEngine.Rendering;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class BaseGOData
    {
        public MineData MineData;
        public SerializedDictionary<string, RoomData> RoomsData;
    }
}