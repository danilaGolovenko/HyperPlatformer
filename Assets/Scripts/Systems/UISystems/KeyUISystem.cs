using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class KeyUISystem : BaseSystem, IHaveActor, ILateStart
    {
        private KeysUIMonoComponent keysUIMonoComponent;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out keysUIMonoComponent);
        }

        public IActor Actor { get; set; }
        public void LateStart()
        {
            // IEntity player = Owner.World.GetSingleComponent<PlayerTagComponent>().Owner;
            // player.TryGetHecsComponent(out PlayerKeysAmountComponent playerKeysAmountComponent);
            // playerKeysAmountComponent.amount.OnChange += CurrentWinPointsAmountOnChange;
            // keysUIMonoComponent.InitKeyUI(playerKeysAmountComponent);
        }
        
        private void CurrentWinPointsAmountOnChange(int obj)
        {
            keysUIMonoComponent.UpdateCurrentAmount();
        }
    }
}