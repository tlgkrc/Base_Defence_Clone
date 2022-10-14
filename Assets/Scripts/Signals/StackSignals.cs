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
        public UnityAction<int> onAddStack = delegate {  };
        public UnityAction<Transform> onClearStaticStack = delegate {  };
        public UnityAction onClearDynamicStack = delegate {  };
        public UnityAction<GameObject> onAddHostageToStack = delegate {  };
        public UnityAction<GameObject> onPlayerGameObject = delegate { };
        public UnityAction<bool> onLastCollectableAddedToPlayer = delegate {  };
        public Func<int> onGetCurrentScore = delegate { return 1; };

    }
}