using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class IncreaseSpeedAfterHalfLifeSystem : BaseSystem, IFixedUpdatable, IHaveActor
    {
        [Required] public HealthComponent healthComponent;
        private bool isIncreased = false;

        public IActor Actor { get; set; }

        public override void InitSystem()
        {
        }

        public void FixedUpdateLocal()
        {
            if (!isIncreased && healthComponent.currentHealth.CurrentValue < healthComponent.maxHealth / 3)
            {
                IncreaseSpeed();
                isIncreased = true;
            }
        }

        private void IncreaseSpeed() {

            var speedCoeffComponent = Owner.GetSpeedCoeffComponent();
            speedCoeffComponent.coefficient *= 2;
            speedCoeffComponent.defaultCoefficient *= 2;

            var abilitiesWithCooldown  = Owner.World.Filter(HMasks.AbilityCooldownAmountComponent).Data;
            foreach(var ability in abilitiesWithCooldown)
            {
                if (ability == null) continue;
                ability.GetAbilityCooldownAmountComponent().TimeInSeconds /= 2;
            }

            Actor.TryGetComponent(out Animator animator);
            animator.speed *= 2;

            AbilitiesMap.AbilitiesToIdentifiersMap.TryGetValue(AbilitiesMap.ChargeAbilityContainer_string, out int abilityContaierId);
            Owner.GetAbilitiesHolderComponent().IndexToAbility.TryGetValue(abilityContaierId, out IEntity chargeAbility);
            chargeAbility.GetChargeAbilityComponent().ChargeSpeed /= 2;
        }
    }
}