using System;
using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public UnityAction<GameObject> onUpdateStageArea = delegate {  };
        public UnityAction<bool> onPlayerInBase = delegate{  };
        public  UnityAction onSetPlayerToTurretShooter = delegate {  };
        public UnityAction onReleasePlayer = delegate {  };
        public Func<float> onSetTurretRotation = delegate { return 0; };
        public UnityAction<TurretTransformParams> onSetPlayerTransformAtTurret = delegate {  };
        public UnityAction onOpenTurretWorker = delegate {  };
    }
}