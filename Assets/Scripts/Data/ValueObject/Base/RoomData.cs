using System;
using Interfaces;

namespace Data.ValueObject.Base
{
    [Serializable]
    public class RoomData : ISaveableEntity
    {
        public int Cost;
        public int PaidAmount;
        public float BuyDelay;
        public string GetKey()
        {
            throw new NotImplementedException();
        }
    }
}