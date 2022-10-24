using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction onChangeGameState = delegate {  };//daha sonra onlevelsuccessfull a baglanacak
        public UnityAction<CameraStates> onSetCameraState = delegate {  };
    }
}