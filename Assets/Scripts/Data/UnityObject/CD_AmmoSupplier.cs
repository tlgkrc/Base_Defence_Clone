using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AmmoSupplier", menuName = "Game/CD_AmmoSupplier", order = 0)]
    public class CD_AmmoSupplier : ScriptableObject
    {
        public AmmoSupplierData AmmoSupplierData;
    }
}