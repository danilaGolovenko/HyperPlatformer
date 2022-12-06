using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PunchAbilitySystem : BaseAbilitySystem
    {
        // [Required] private PunchAbilityComponent abilityComponent;
        public override void InitSystem()
        {
        }
        
        private void FlipToCorrectDirection(IEntity owner, IEntity target)
        {
            ((Actor)owner).TryGetComponent(out SpriteRenderer spriteRenderer);
            spriteRenderer.flipX = target?.GetUnityTransformComponent().Transform.transform.position.x >
                                   owner?.GetUnityTransformComponent().Transform.transform.position.x;
        }

        public override void Execute(IEntity owner = null, IEntity target = null, bool enable = true)
        {
            Owner.TryGetHecsComponent(out PunchAbilityComponent punchAbilityComponent);
            Owner.TryGetHecsComponent(out AbilityCooldownAmountComponent cooldownAmountComponent);
            FlipToCorrectDirection(owner, target);
            
            var commandIsAttacking = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isPunchAttacking,
                Value = true
            };
            owner?.Command(commandIsAttacking);

            target?.Command(new DamageCommand()
            {
                amount = punchAbilityComponent.AttackPower,
                authorEntity = owner
            });
            
            var cooldownComponent = Owner.GetOrAddComponent<CooldownComponent>();
            if (cooldownComponent != null) cooldownComponent.CurrentTime = cooldownAmountComponent.TimeInSeconds;

            var ownerCooldownComponent = owner.GetOrAddComponent<CooldownComponent>();
            owner.TryGetHecsComponent(out AbilityCooldownAmountComponent ownerCooldownAmountComponent);
            if (ownerCooldownComponent != null) ownerCooldownComponent.CurrentTime = ownerCooldownAmountComponent.TimeInSeconds;
        }
    }
}