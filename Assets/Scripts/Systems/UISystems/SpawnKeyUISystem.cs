using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class SpawnKeyUISystem : BaseSystem, IReactComponentGlobal<PlayerHolderComponent>
    {
        public override void InitSystem()
        {
        }

        public void ComponentReactGlobal(PlayerHolderComponent component, bool isAdded)
        {
            if (isAdded)
            {
                ShowUICommand showUICommand = new ShowUICommand();
                showUICommand.UIViewType = UIIdentifierMap.KeyUI_identifier;
                Owner.World.Command(showUICommand);
            }
            else
            {
                HideUICommand hideUICommand = new HideUICommand();
                hideUICommand.UIViewType = UIIdentifierMap.KeyUI_identifier;
                Owner.World.Command(hideUICommand);
            }
        }
    }
}