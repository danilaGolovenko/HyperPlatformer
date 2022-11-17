using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class DeathHandlerSystem : BaseSystem, IReactCommand<Commands.DeathCommand>, IReactCommand<Commands.EventStateAnimationCommand>
    {
        
        //TODO 
        // АДО СДЕЛАТЬ ЕЕ ЛОКАЛЬНОЙ
        public override void InitSystem()
        {
        }

        public void CommandReact(DeathCommand command)
        {
            // Debug.Log("============================== " + command.entity + " --- RIP ==============================");
            BoolAnimationCommand commandIsDead = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isDead,
                Value = true
            };
            Owner.Command(commandIsDead);
            if (Owner.TryGetHecsComponent(out InputListenerTagComponent inputListenerTag))
            {
                Owner.RemoveHecsComponent<InputListenerTagComponent>();
            }
            if (Owner.TryGetHecsComponent(out HealthBarTagComponent healthBarTag))
            {
                Owner.RemoveHecsComponent<HealthBarTagComponent>();
            }
        }

        public void CommandReact(EventStateAnimationCommand command)
        {
            if (command.StateId == AnimatorStateIdentifierMap.SpitterDeath || command.StateId == AnimatorStateIdentifierMap.Ellen_Death)
            {
                DestroyEntityWorldCommand destroyEntityWorldCommand = new DestroyEntityWorldCommand();
                destroyEntityWorldCommand.Entity = Owner;
                Owner.World.Command(destroyEntityWorldCommand);
            }
        }
    }
}