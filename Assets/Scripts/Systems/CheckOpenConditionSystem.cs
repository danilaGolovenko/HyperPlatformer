using System;
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
        [Required] private RequiredAmountOfKeysComponent requiredAmountOfKeysComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.gameObject.TryGetComponent(out Actor actor) && 
                actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent)){
                if (playerHolderComponent.PlayerEntity.GetPlayerKeysAmountComponent().amount.CurrentValue >= requiredAmountOfKeysComponent.amount){
                     Owner.World.Command(new WinCommand());
                }
                else
                {
                    ShowUICommand showUICommand = new ShowUICommand();
                    showUICommand.UIViewType = UIIdentifierMap.DialogueUI_identifier;
                    Owner.World.Command(showUICommand);
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