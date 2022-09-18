using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public UnityAction<GameObject> onUpdateStageArea = delegate {  };
    }
}