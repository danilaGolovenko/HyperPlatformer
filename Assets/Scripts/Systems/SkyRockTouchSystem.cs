using System;
using Commands;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using Helpers;

namespace Systems
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class SkyRockTouchSystem : BaseSystem, IHaveActor, IReactCommand<Trigger2dEnterCommand>
    {
        [Required] private SkyRockComponent skyRockComponent;
        public override void InitSystem()
        {
        }
        
        public IActor Actor { get; set; }
        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.TryGetActorFromCollision(out var actor) &&
                actor.ContainsMask(ref HMasks.PlayerHolderComponent))
            {
                actor.Command(new DamageCommand()
                {
                    amount = skyRockComponent.AttackPower, 
                    authorEntity = Owner
                });
                Owner.HecsDestroy();
            }
        }
    }
}