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
        public override void InitSystem()
        {
        }

        public void CommandReact(DeathCommand command)
        {
            var commandIsDead = new BoolAnimationCommand
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

            if (Owner.TryGetHecsComponent(out EnemyTagComponent enemyTag))
            {
                Owner.World.Command(new IncreaseKilledEnemiesAmountCommand());
            }
        }

        public void CommandReact(EventStateAnimationCommand command)
        {
            if (command.StateId == AnimatorStateIdentifierMap.SpitterDeath || command.StateId == AnimatorStateIdentifierMap.Ellen_Death || command.StateId == AnimatorStateIdentifierMap.ChomperDeath)
            {
                DestroyEntityWorldCommand destroyEntityWorldCommand = new DestroyEntityWorldCommand();
                destroyEntityWorldCommand.Entity = Owner;
                Owner.World.Command(destroyEntityWorldCommand);
            }
            // todo переделать. 1) это не ответственность данной системы. 2) при смерти одного спавняться все
            if (command.StateId == AnimatorStateIdentifierMap.SpitterDeath || command.StateId == AnimatorStateIdentifierMap.ChomperDeath)
            {
                Owner.World.Command(new EnemySpawnCommand());
            }
        }
    }
}