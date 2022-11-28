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
            player.TryGetHecsComponent(out KilledEnemiesComponent winPointsComponent);
            winPointsComponent.CurrentAmount.OnChange += CurrentWinPointsAmountOnChange;
            killedEnemiesUIMonoComponent.InitKilledEnemiesUI(winPointsComponent);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            if (Owner.World.TryGetSingleComponent(out PlayerTagComponent playerTagComponent))
            {
                playerTagComponent.Owner.TryGetHecsComponent(out KilledEnemiesComponent killedEnemiesComponent);
                killedEnemiesComponent.CurrentAmount.OnChange -= CurrentWinPointsAmountOnChange;
            }
        }
        
        private void CurrentWinPointsAmountOnChange(int obj)
        {
            killedEnemiesUIMonoComponent.UpdateCurrentAmount();
        }
    }
}