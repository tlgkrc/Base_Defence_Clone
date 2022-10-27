using System;
using System.Collections.Generic;
using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class BaseSignals : MonoSingleton<BaseSignals>
    {
        public UnityAction<bool> onPlayerInBase = delegate{  };
        public  UnityAction onSetPlayerToTurretShooter = delegate {  };
        public UnityAction onReleasePlayer = delegate {  };
        public Func<float> onSetTurretRotation = () => 0;
        public UnityAction<TurretTransformParams> onSetPlayerTransformAtTurret = delegate {  };
        public UnityAction<int> onSetBaseLevelText =delegate{  };
        public Func<List<Transform>> onSetMineTransforms = () => null;
        public Func<Transform> onSetGemStock = () => null;
        public Func<List<Transform>> onSetAmmoStockTransforms = () => null;
        public Func<Transform> onSetAmmoDepotTransform = () => null;

        public UnityAction onOpenTurretWorker = delegate {  };
    }
}