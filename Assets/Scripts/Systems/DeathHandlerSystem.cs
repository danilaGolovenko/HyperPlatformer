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
            if (command.AnimationId == AnimationEventIdentifierMap.EndClip && (command.StateId == AnimatorStateIdentifierMap.SpitterDeath || command.StateId == AnimatorStateIdentifierMap.Ellen_Death || command.StateId == AnimatorStateIdentifierMap.ChomperDeath ||
                command.StateId == AnimatorStateIdentifierMap.Boss_Death))
            {
                var destroyEntityWorldCommand = new DestroyEntityWorldCommand
                {
                    Entity = Owner
                };
                Owner.World.Command(destroyEntityWorldCommand);

                if (command.StateId == AnimatorStateIdentifierMap.Ellen_Death)
                {
                    Owner.World.Command(new ShowUICommand()
                    {
                        UIViewType = UIIdentifierMap.GameOverCanvas_UIIdentifier
                    });
                }

            }
            // todo ????????????????????. 1) ?????? ???? ?????????????????????????????? ???????????? ??????????????
            if (command.StateId == AnimatorStateIdentifierMap.SpitterDeath || command.StateId == AnimatorStateIdentifierMap.ChomperDeath)
            {
                Owner.Command(new EnemySpawnCommand());
            }
        }
    }
}