using Controllers;
using Signals;
using Enums;
using Data.UnityObject;
using Data.ValueObject;
using UnityEngine;
using System.Collections;

public class CollectableManager : MonoBehaviour
{
    #region Self Variables
    #region Public Variables

    #endregion
    #region SerializeField Variables

    [SerializeField] private ZombieAnimStates initialAnimState;
    #endregion
    #region Private Variables
    
    private CollectableData _data;
    
    #endregion
    #endregion

    private void Awake()
    {
        _data = GetCollectableData();
    }
    

    private CollectableData GetCollectableData() => Resources.Load<CD_Collectable>("Data/CD_Collectable").Data;

    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {

    }

    private void UnsubscribeEvents()
    {

    }
    
    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion
}