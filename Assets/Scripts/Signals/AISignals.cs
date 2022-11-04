using System;
using System.Collections.Generic;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class AISignals : MonoSingleton<AISignals>
    {
        public UnityAction onSetEnemyTarget = delegate {  };
        public UnityAction onTriggerThrowEvent = delegate {  };
        public UnityAction onTriggerFakeHoldEvent = delegate {  };
        public UnityAction onFinishExplosion = delegate {  };
        public UnityAction<Transform> onSetOpenedTurret = delegate {  };
        public UnityAction<Transform> onPlaceNewHostage = delegate{  };
        public UnityAction<int> onSetPlayerToTurretShooter = delegate {  };
        
        public Func<int> onSetMaxMiner = delegate { return 0; };
        public Func<List<Transform>> onSetMineTransforms = () => null;
    }
}