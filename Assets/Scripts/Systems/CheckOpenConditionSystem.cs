using System;
using System.Collections.Generic;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Helpers;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class CheckOpenConditionSystem : BaseSystem, IReactCommand<Trigger2dEnterCommand>, IReactCommand<Trigger2dExitCommand>
    {
        [Required] private RequiredItemsComponent requiredItemsComponent;
        private string requiredItemsString = "";
        public override void InitSystem()
        {
            foreach (var item in requiredItemsComponent.itemsList)
            {
                requiredItemsString += "\n" + item.itemContainer.GetComponent<DescriptionComponent>().description +
                                       " in the amount of " + item.amount;
            }
        }

        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.TryGetActorFromCollision(out var actor) && 
                actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent)){
                var playerInventory = playerHolderComponent.PlayerEntity.GetPlayerInventoryComponent().currentItems;
                bool playerHasAllRequiredItems = true;
                foreach(Item item in requiredItemsComponent.itemsList)
                {
                    if (!playerInventory.ContainsKey(item.itemContainer.ContainerIndex) ||
                        playerInventory[item.itemContainer.ContainerIndex] < item.amount)
                    {
                        playerHasAllRequiredItems = false;
                        break;
                    }
                }
                if (playerHasAllRequiredItems)
                {
                    Owner.World.Command(new WinCommand());
                }
                else
                {
                    ShowUICommand showUICommand = new ShowUICommand();
                    showUICommand.UIViewType = UIIdentifierMap.DialogueUI_identifier;
                    showUICommand.OnUILoad += (ui) =>
                    {
                        InitDialogueUITextCommand initDialogueUITextCommand = new InitDialogueUITextCommand
                        {
                            DialogueText = "To open the door you need: " + requiredItemsString
                                           
                        };
                        ui.Command(initDialogueUITextCommand);
                    };
                    Owner.World.Command(showUICommand);
                }
            }
        }

        public void CommandReact(Trigger2dExitCommand command)
        {
            var hideUICommand = new HideUICommand
            {
                UIViewType = UIIdentifierMap.DialogueUI_identifier
            };
            Owner.World.Command(hideUICommand);
        }
    }
}