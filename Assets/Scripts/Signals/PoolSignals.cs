using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PoolSignals: MonoSingleton<PoolSignals>
    {
        public UnityAction<string,GameObject> onReleasePoolObject = delegate {  };
        
        public Func<string,Transform,GameObject> onGetPoolObject = (type, transform1) => default;
    }
}