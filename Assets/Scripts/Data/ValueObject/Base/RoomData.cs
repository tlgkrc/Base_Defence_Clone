using System;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class RoomData
    {
        public int Cost;
        public bool isPaid;
        public TurretData TurretData;
    }
}