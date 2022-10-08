using System;
using Data.UnityObject;
using Data.ValueObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI.Subscribers
{
    public class Enemy : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject enemyGameObject;

        #endregion

        #region Private Variables

        [ShowInInspector] private EnemyGOData _enemyGoData;

        #endregion

        #endregion

        private void Awake()
        {
            _enemyGoData = GetEnemyData();
        }

        private EnemyGOData GetEnemyData() =>
            Resources.Load<CD_EnemyData>("Data/CD_EnemyData").EnemyDatas.Datas[enemyGameObject];
        
        
    }
}