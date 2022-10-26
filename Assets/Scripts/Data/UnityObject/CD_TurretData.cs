using Data.ValueObject.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_TurretData", menuName = "Game/CD_TurretData", order = 0)]
    public class CD_TurretData : SerializedScriptableObject
    {
        public TurretData TurretData;
    }
}