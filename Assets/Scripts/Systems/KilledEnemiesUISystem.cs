using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class KilledEnemiesUISystem : BaseSystem, IHaveActor, ILateStart
    {
        private KilledEnemiesUIMonoComponent killedEnemiesUIMonoComponent;

        public override void InitSystem()
        {
            Actor.TryGetComponent(out killedEnemiesUIMonoComponent);
        }

        public IActor Actor { get; set; }
        public void LateStart()
        {
            IEntity player = Owner.World.GetSingleComponent<PlayerTagComponent>().Owner;
            player.TryGetHecsComponent(out WinPointsComponent winPointsComponent);
            winPointsComponent.currentAmount.OnChange += CurrentWinPointsAmountOnChange;
            killedEnemiesUIMonoComponent.InitKilledEnemiesUI(winPointsComponent);
        }
        
        private void CurrentWinPointsAmountOnChange(int obj)
        {
            killedEnemiesUIMonoComponent.UpdateCurrentAmount();
        }
    }
}