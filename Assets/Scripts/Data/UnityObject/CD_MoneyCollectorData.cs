using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_MoneyCollectorData", menuName = "Game/CD_MoneyCollectorData", order = 0)]
    public class CD_MoneyCollectorData : ScriptableObject
    {
        public MoneyCollectorData MoneyCollectorData;
    }
}