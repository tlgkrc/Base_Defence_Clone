using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Commands
{
    public class QueryPointerOverUIElementCommand
    {
        public bool Execute()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}