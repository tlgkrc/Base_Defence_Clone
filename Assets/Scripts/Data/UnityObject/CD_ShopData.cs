using Data.ValueObject.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_ShopData", menuName = "Game/CD_ShopData", order = 0)]
    public class CD_ShopData : SerializedScriptableObject
    {
        public ShopData ShopData;
    }
}