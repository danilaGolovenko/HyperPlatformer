using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class RockfallAbilitySystem : BaseAbilitySystem
    {
        // [Required] public RockfallAbilityComponent abilityComponent;
        public override void InitSystem()
        {
        }

        public override void Execute(IEntity owner = null, IEntity target = null, bool enable = true)
        {
            Owner.TryGetHecsComponent(out AbilityCooldownAmountComponent cooldownAmountComponent);
            var commandIsRockfalling = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isRockfalling,
                Value = true
            };
            owner?.Command(commandIsRockfalling);
            
            var cooldownComponent = Owner.GetOrAddComponent<CooldownComponent>();
            if (cooldownComponent != null) cooldownComponent.CurrentTime = cooldownAmountComponent.TimeInSeconds;

            var ownerCooldownComponent = owner.GetOrAddComponent<CooldownComponent>();
            owner.TryGetHecsComponent(out AbilityCooldownAmountComponent ownerCooldownAmountComponent);
            if (ownerCooldownComponent != null) ownerCooldownComponent.CurrentTime = ownerCooldownAmountComponent.TimeInSeconds;
        }
    }
}