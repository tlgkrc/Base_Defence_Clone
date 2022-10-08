using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_HostageStackData", menuName = "Game/CD_HostageStackData", order = 0)]
    public class CD_HostageStackData : ScriptableObject
    {
        public HostageStackData HostageStackData;
    }
}