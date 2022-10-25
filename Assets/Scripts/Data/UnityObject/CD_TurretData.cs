using Data.ValueObject.Base;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_TurretData", menuName = "Game/CD_TurretData", order = 0)]
    public class CD_TurretData : ScriptableObject
    {
        public TurretData TurretData;
    }
}