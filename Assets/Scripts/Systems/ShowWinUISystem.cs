using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Unity.VisualScripting;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class ShowWinUISystem : BaseSystem, IReactGlobalCommand<Commands.WinCommand>
    {
        public override void InitSystem()
        {
           
        }

        public void CommandGlobalReact(WinCommand command)
        {
            ShowUICommand showUICommand = new ShowUICommand();
            showUICommand.UIViewType = UIIdentifierMap.WinUI_identifier;
            Owner.World.Command(showUICommand);

            var playerList = Owner.World.Filter(HMasks.PlayerTagComponent);
            var player = playerList.Data[0];
            player.RemoveHecsSystem<MovingSystem>();
        }
    }
}