using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class KilledEnemiesSystem : BaseSystem, IReactGlobalCommand<Commands.IncreaseKilledEnemiesAmountCommand>
    {
        [Required] private PlayerHolderComponent playerHolderComponent;
        private KilledEnemiesComponent killedEnemiesComponent;
        public override void InitSystem()
        {
            killedEnemiesComponent = playerHolderComponent.PlayerEntity.GetKilledEnemiesComponent();
        }

        public void CommandGlobalReact(IncreaseKilledEnemiesAmountCommand command)
        {
            killedEnemiesComponent.CurrentAmount.CurrentValue++;
        }
    }
}