using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class WinPointsSystem : BaseSystem, IReactGlobalCommand<Commands.IncreaseWinPointsCommand>
    {
        [Required] private WinPointsComponent winPointsComponent;
        public override void InitSystem()
        {
        }

        public void CommandGlobalReact(IncreaseWinPointsCommand command)
        {
            winPointsComponent.currentAmount++;
        }
    }
}