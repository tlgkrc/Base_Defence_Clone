using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject;
using Signals;

namespace Commands
{
    public class ItemAddOnStackCommand
    {
        #region Self Variables
        #region Private Variables
        private List<GameObject> _hostageStack;
        private Transform _transform;
        private HostageStackData _hostageStackData;
        #endregion
        #endregion
        
        public ItemAddOnStackCommand(ref List<GameObject> hostageStack,Transform transform,HostageStackData hostageStackData)
        {
            _hostageStack = hostageStack;
            _transform = transform;
            _hostageStackData = hostageStackData;
        }
        
        public void Execute(GameObject hostageGO)
        {
            if (_hostageStack.Count == 0)
            {
                _hostageStack.Add(hostageGO);
                hostageGO.transform.SetParent(_transform);
                hostageGO.transform.localPosition = Vector3.zero;
            }
            else
            {
                hostageGO.transform.SetParent(_transform);
                Vector3 newPos = _hostageStack[_hostageStack.Count - 1].transform.localPosition;
                newPos.z -= _hostageStackData.HostageOffsetInStack;
                hostageGO.transform.localPosition = newPos;
                _hostageStack.Add(hostageGO);
            }
            hostageGO.transform.LookAt(_transform);
           
        }
    }
}