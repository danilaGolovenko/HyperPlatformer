using System;
using System.Collections.Generic;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class CheckOpenConditionSystem : BaseSystem, IReactCommand<Trigger2dEnterCommand>, IReactCommand<Trigger2dExitCommand>
    {
        [Required] private RequiredItemsComponent requiredItemsComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.gameObject.TryGetComponent(out Actor actor) && 
                actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent)){
                var playerInventory = playerHolderComponent.PlayerEntity.GetPlayerInventoryComponent().currentItems;
                foreach(KeyValuePair<int, int> kvp in requiredItemsComponent.requiredItemsDictionary)
                {
                    if (playerInventory.ContainsKey(kvp.Key) && playerInventory[kvp.Key] >= kvp.Value){
                        Owner.World.Command(new WinCommand());
                    }
                    else
                    {
                        // todo диалоговое окно должно сообщать об отсутствии конкретных вещей, необходимых для этой двери
                        ShowUICommand showUICommand = new ShowUICommand();
                        showUICommand.UIViewType = UIIdentifierMap.DialogueUI_identifier;
                        Owner.World.Command(showUICommand);
                    }
                }   
            }
        }

        public void CommandReact(Trigger2dExitCommand command)
        {
            HideUICommand hideUICommand = new HideUICommand();
            hideUICommand.UIViewType = UIIdentifierMap.DialogueUI_identifier;
            Owner.World.Command(hideUICommand);
        }
    }
}