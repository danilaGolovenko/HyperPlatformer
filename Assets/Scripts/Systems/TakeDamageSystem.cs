using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class TakeDamageSystem : BaseSystem, IReactCommand<Commands.DamageCommand>
    {
        [Required] private HealthComponent healthComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(DamageCommand command)
        {
            if (!command.authorEntity.Equals(Owner))
            {
                Debug.Log("Damage from " + command.authorEntity + " to " + Owner + " --- " + command.amount);
                healthComponent.currentHealth -= command.amount;
                if (healthComponent.currentHealth <= 0)
                {
                    DeathCommand deathCommand = new DeathCommand();
                    deathCommand.entity = Owner;
                    Owner.World.Command(deathCommand);
                }
                Debug.Log("Current " + Owner + " health is" + healthComponent.currentHealth);
            }
        }
    }
}