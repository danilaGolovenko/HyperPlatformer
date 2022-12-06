using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Commands;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class ChargeAbilitySystem : BaseAbilitySystem, IAfterEntityInit, IReactCommand<Commands.ChargeDamageCommand>
    {
        [Required] public ChargeAbilityComponent chargeAbilityComponent;
        private AbilityCooldownAmountComponent cooldownAmountComponent;
        private SpriteRenderer spriteRenderer;
        private IEntity targetHolder;
       

        public override void InitSystem()
        {
        }
        public void AfterEntityInit()
        {
            var owner = (IActor)Owner.GetAbilityOwnerComponent().AbilityOwner;
            owner?.TryGetComponent(out spriteRenderer);
            Owner.TryGetHecsComponent(out cooldownAmountComponent);
        }

        private void FlipToCorrectDirection(IEntity owner, IEntity target)
        {
            spriteRenderer.flipX = target?.GetUnityTransformComponent().Transform.transform.position.x >
                                   owner?.GetUnityTransformComponent().Transform.transform.position.x;
        }

        public override void Execute(IEntity owner = null, IEntity target = null, bool enable = true)
        {
            if (target == null) return;
            targetHolder = target;

            FlipToCorrectDirection(owner, target);


            owner?.AddHecsSystem(new ChargeMovingSystem());
            owner?.Command(new StartChargeCommand()
            {
                GoalPointX = target.GetUnityTransformComponent().Transform.transform.position.x,
                SpeedCoeff = chargeAbilityComponent.ChargeSpeed
            });

            var abilityCooldownComponent = Owner.GetOrAddComponent<CooldownComponent>();
            if (abilityCooldownComponent != null) abilityCooldownComponent.CurrentTime = cooldownAmountComponent.TimeInSeconds;

            var ownerCooldownComponent = owner.GetOrAddComponent<CooldownComponent>();
            owner.TryGetHecsComponent(out AbilityCooldownAmountComponent ownerCooldownAmountComponent);
            if (ownerCooldownComponent != null) ownerCooldownComponent.CurrentTime = ownerCooldownAmountComponent.TimeInSeconds;
        }

        public void CommandReact(ChargeDamageCommand command)
        {
            if (targetHolder == null || targetHolder.ComponentContext == null) return;
            var targetX = targetHolder.GetUnityTransformComponent().Transform.transform.position.x;
            var owner = Owner.GetAbilityOwnerComponent().AbilityOwner;
            var ownerX = owner.GetUnityTransformComponent().Transform.transform.position.x;
            if (Math.Abs(targetX - ownerX) <= chargeAbilityComponent.AttackRangeRadius)
                targetHolder.Command(new DamageCommand()
                {
                    amount = chargeAbilityComponent.AttackPower,
                    authorEntity = Owner.GetAbilityOwnerComponent().AbilityOwner
                });
        }
    }
}