using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onReset = delegate { };
        public UnityAction<CameraStates> onSetCameraState = delegate {  };
        public UnityAction<Transform> onSetCameraAtTurret = delegate {  };
        public UnityAction<WeaponTypes> onSetHoldingGun = delegate(WeaponTypes arg0) {  };//Will be added to shopManager
    }
}