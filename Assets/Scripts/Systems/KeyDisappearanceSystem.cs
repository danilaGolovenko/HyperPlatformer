using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class KeyDisappearanceSystem : BaseSystem, IReactCommand<Commands.EventStateAnimationCommand>
    {
        public override void InitSystem()
        {
        }
        
        public void CommandReact(EventStateAnimationCommand command)
        {
            if (command.StateId == AnimatorStateIdentifierMap.KeyPickUp)
            {
                HideUICommand hideUICommand = new HideUICommand
                {
                    UIViewType = UIIdentifierMap.DialogueUI_identifier
                };
                Owner.World.Command(hideUICommand);
                
                DestroyEntityWorldCommand destroyEntityWorldCommand = new DestroyEntityWorldCommand
                {
                    Entity = Owner
                };
                Owner.World.Command(destroyEntityWorldCommand);
            }
        }
    }
}