using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Helpers;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class WarheadTouchSystem : BaseSystem, IHaveActor, IReactCommand<Trigger2dEnterCommand>
    {
        [Required] private WarheadComponent warheadComponent;
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
                    amount = warheadComponent.AttackPower, 
                    authorEntity = Owner
                });
                Owner.HecsDestroy();
            }
        }
    }
}