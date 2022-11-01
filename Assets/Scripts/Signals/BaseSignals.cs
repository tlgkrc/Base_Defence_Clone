﻿using System;
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
        public  UnityAction<int> onSetPlayerToTurretShooter = delegate {  };
        public UnityAction onReleasePlayer = delegate {  };
        public Func<float> onSetTurretRotation = () => 0;
        public UnityAction<TurretTransformParams> onSetPlayerTransformAtTurret = delegate {  };
        public UnityAction<int> onSetBaseLevelText =delegate{  };
        public Func<List<Transform>> onSetMineTransforms = () => null;
        public Func<List<Transform>> onSetBaseTargetTransforms = () => null;
        public Func<Transform> onSetAmmoStockTransform = () => null;
        public UnityAction<Transform> onPlaceNewHostage = delegate{  };
        public Func<Transform> onSetGemStock = () => null;
        public Func<Transform> onSetAmmoDepotTransform = () => null;
        public Func<int> onSetMaxMiner = delegate { return 0; };
        public UnityAction onAddMiner = delegate {  };
        public UnityAction<Vector3> onSetThrowingStar = delegate {  };//sprite affect animation
        public UnityAction<Vector3> onSetThrowForce = delegate {  };
        public Func<Transform> onSetBaseTransform = delegate { return null; };
        public UnityAction onSetEnemyTarget = delegate {  };
        public UnityAction onOpenTurretWorker = delegate {  };
    }
}