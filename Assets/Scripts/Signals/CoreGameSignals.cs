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
        public UnityAction onCheckCloseEnemy = delegate {  };
        public UnityAction<bool> onChangedFootAnim =delegate {  };
        public UnityAction<int> onUpdatePlayerHealth = delegate {  };
        public UnityAction<int> onSetGrenadeDamage = delegate {  };
        public UnityAction<Transform> onSetCameraAtTurret = delegate {  };
        public UnityAction<GameObject> onDieEnemy = delegate {  };
        public UnityAction<WeaponTypes> onSetHoldingGun = delegate(WeaponTypes arg0) {  };
        public UnityAction<CameraStates> onSetCameraState = delegate {  };

        public Func<int> onSetTurretBulletDamage = delegate { return 0; };
        public Func<Transform> onGetPlayerTransform = delegate { return null; };
        public Func<WeaponTypes,int> onSetWeaponBulletDamage = delegate { return 0; };
    }
}