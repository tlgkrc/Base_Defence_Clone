using System;
using Enums;
using UnityEngine;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class EnemyDatas
    {
        public SerializedDictionary<GameObject, EnemyGOData> Datas;
    }
}