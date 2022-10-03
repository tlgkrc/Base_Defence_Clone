using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PoolSignals: MonoSingleton<PoolSignals>
    {
        public Func<string,Transform,GameObject> onGetPoolObject = (type, transform1) => default;
        public UnityAction<string,GameObject> onReleasePoolObject = delegate {  };
    }
}