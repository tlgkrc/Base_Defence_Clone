using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class StackSignals: MonoSingleton<StackSignals>
    {
        public UnityAction onAddStack = delegate {  };
        public UnityAction<Transform> onClearStack = delegate {  };
        public UnityAction<bool> onLastCollectableAddedToPlayer = delegate {  };
        public Func<int> onGetCurrentScore = delegate { return 1; };

    }
}