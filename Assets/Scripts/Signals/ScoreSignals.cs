using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> onSetScore = delegate { };
        public UnityAction<int> onUpdateMoneyScore = delegate {  };
        public UnityAction<int> onUpdateDiamonScore = delegate { };
        public Func<int> onGetMoneyScore =delegate { return 0; };
        public Func<int> onGetDiamondScore = delegate { return 0; };
    }
}