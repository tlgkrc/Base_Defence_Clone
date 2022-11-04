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
        public UnityAction onOpenTurretWorker = delegate {  };
        public UnityAction onReleasePlayer = delegate {  };
        public UnityAction<bool> onPlayerInBase = delegate{  };
        public UnityAction<int> onSetBaseLevelText =delegate{  };
        public UnityAction<Transform> onAddMiner = delegate {  };
        public UnityAction<Vector3> onSetThrowingStar = delegate {  };
        public UnityAction<TurretTransformParams> onSetPlayerTransformAtTurret = delegate {  };
        public UnityAction<Vector3,int> onSetThrowForce = delegate {  };
        
        public Func<float> onSetTurretRotation = () => 0;
        public Func<Transform> onSetBaseTransform = delegate { return null; };
        public Func<Transform> onSetAmmoStockTransform = () => null;
        public Func<Transform> onSetGemStock = () => null;
        public Func<Transform> onSetAmmoDepotTransform = () => null;
        public Func<List<Transform>> onSetBaseTargetTransforms = () => null;
    }
}