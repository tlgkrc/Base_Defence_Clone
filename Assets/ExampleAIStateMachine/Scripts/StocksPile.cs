using System;
using UnityEngine;


namespace ExampleAIStateMachine.Scripts
{
    [ExecuteInEditMode]
    public class StocksPile : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] 

        #endregion

        #region Private Variables

        private int _collected;

        #endregion

        #endregion

        private void Awake()
        {
            _collected = 0;
            Add();
        }

        public void Add()
        {
            _collected++;
        
        }
    }
}