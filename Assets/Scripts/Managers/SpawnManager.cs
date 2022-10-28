using System.Collections;
using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Transform> hostagePlaceTransforms;
        [SerializeField] private List<Transform> enemyCreateTransforms;
        [SerializeField] private Transform buyAmmoWorkerTransform;
        [SerializeField] private Transform buyMoneyWorkerTransform;

        #endregion

        #region Private Variables

        private SpawnGOData _spawnGoData;
        private bool _isFinishedLevel;

        #endregion

        #endregion

        private void Awake()
        {
            _spawnGoData = GetSpawnData();
            _isFinishedLevel = false;
        }

        private SpawnGOData GetSpawnData()
        {
            return Resources.Load<CD_SpawnData>("Data/CD_SpawnData").SpawnData.SpawnGoData;
        }

        private void Start()
        {
            PlaceHostageToTransforms();
            StartCoroutine(InstantiateWeakEnemies());
            StartCoroutine(InstantiateMidEnemies());
            StartCoroutine(InstantiateProEnemies());
        }

        #region Subscription Events

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BaseSignals.Instance.onPlaceNewHostage += OnPlaceNewHostage;
        }

        private void UnSubscribeEvents()
        {
            BaseSignals.Instance.onPlaceNewHostage -= OnPlaceNewHostage;

        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        private void PlaceHostageToTransforms()
        {
            foreach (var hostageTransform in hostagePlaceTransforms)
            {
                PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Hostage.ToString(), hostageTransform);
            }
        }

        private void OnPlaceNewHostage(Transform hostageTransform)
        {
            //PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Hostage.ToString(), hostageTransform);
        }

        private IEnumerator InstantiateProEnemies()
        {
            while (!_isFinishedLevel)
            {
                var index = Random.Range(0, enemyCreateTransforms.Count);
                PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.ProZombie.ToString(),
                    enemyCreateTransforms[index]);
                yield return new WaitForSeconds(_spawnGoData.SpawnDelayProEnemy);
            }

        }

        private IEnumerator InstantiateMidEnemies()
        {
            while (!_isFinishedLevel)
            {
                var index = Random.Range(0, enemyCreateTransforms.Count);
                PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.MidZombie.ToString(),
                    enemyCreateTransforms[index]);
                yield return new WaitForSeconds(_spawnGoData.SpawnDelayMidEnemy);
            }
        }

        private IEnumerator InstantiateWeakEnemies()
        {
            while (!_isFinishedLevel)
            {
                var index = Random.Range(0, enemyCreateTransforms.Count);
                PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.WeakZombie.ToString(),
                    enemyCreateTransforms[index]);
                yield return new WaitForSeconds(_spawnGoData.SpawnDelayWeakEnemy);
            }
        }
    }
}