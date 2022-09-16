using System;
using System.Collections.Generic;
using Extentions;
using Enums;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        //Runner
        public UnityAction onRunnerSaveData = delegate { };
        public Func<int> onGetRunnerLevelID = delegate { return 0; };
        //Idle
        public UnityAction onIdleSaveData = delegate {  };
        public UnityAction onLoadIdle = delegate {  };
        public Func<int> onIdleLevel = delegate { return 0;};
    }
}