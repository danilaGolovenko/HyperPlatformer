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
    public sealed class DrawPickUpUISystem : BaseSystem, IReactGlobalCommand<Commands.OnPickUpItemCommand>
    {
        public override void InitSystem()
        {
        }
        
        public void CommandGlobalReact(OnPickUpItemCommand command)
        {
            ShowUICommand showUICommand = new ShowUICommand();
            showUICommand.UIViewType = UIIdentifierMap.DialogueUI_identifier;
            showUICommand.OnUILoad += (ui) =>
            {
                InitDialogueUITextCommand initDialogueUITextCommand = new InitDialogueUITextCommand
                {
                    DialogueText = "You pick up " + command.Item.GetDescriptionComponent().description + "."
                };
                ui.Command(initDialogueUITextCommand);
            };
            Owner.World.Command(showUICommand);
        }
    }
}