using System;
using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<int> onSetScoreText = delegate { };
        //
        public UnityAction<int> onSetMoneyText = delegate { };
        public UnityAction<int> onSetDiamondText = delegate { };
        public UnityAction onOpenShopPanel = delegate {  };
        public UnityAction onCloseShopPanel = delegate {  };
        public UnityAction<WeaponTypes> onUpgradeWeapon = delegate{  };

    }
}