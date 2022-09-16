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
        public UnityAction<GameObject> onInteractionObstacle = delegate { };
        public UnityAction<GameObject> onInteractionCollectable = delegate { };
        public UnityAction<GameObject> onPlayerGameObject = delegate { };
        public UnityAction onUpdateType = delegate { };
        public UnityAction onBoostArea = delegate {  };
        public UnityAction<float> onSetPlayerScale = delegate {  };
        public UnityAction<bool> onLastCollectableAddedToPlayer = delegate {  };
        public Func<int> onGetCurrentScore = delegate { return 1; };

    }
}