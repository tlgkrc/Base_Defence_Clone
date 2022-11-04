using System;
using UnityEngine;
using UnityEngine.Events;
using Extentions;
using Managers;

namespace Signals
{
    public class StackSignals: MonoSingleton<StackSignals>
    {
        public UnityAction<bool> onActivateMoveHostageStack = delegate(bool arg0) {  };
        public UnityAction<int> onClearDynamicStack = delegate {  };
        public UnityAction<int> onAddAmmoBoxToPlayer = delegate {  };
        public UnityAction<Transform> onClearStaticStack = delegate {  };
        public UnityAction<GameObject> onAddHostageToStack = delegate {  };
        public UnityAction<GameObject> onPlayerGameObject = delegate { };
        public UnityAction<GameObject> onRemoveHostageFromStack = delegate {  };
        public UnityAction<int,int> onDeliverAmmoBox = delegate{  };
        public UnityAction<int,string> onRemoveLastElement =delegate {  };
        public UnityAction<int,GameObject> onAddStack = delegate {  };
        public UnityAction<int,GameObject> onAddMoneyToPlayer = delegate {  };
        public UnityAction<int,StackManager,StackManager> onTransferBetweenStacks = delegate {  };
        
        public Func<int> onGetMaxPlayerStackCount = delegate { return 0; };
    }
}