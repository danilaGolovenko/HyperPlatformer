using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class WaterDamageSystem : BaseSystem, IHaveActor, IReactCommand<Trigger2dEnterCommand>
    {
        [Required] private DamageAmountComponent damageAmountComponent;
        private BoxCollider2D boxCollider2D;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out boxCollider2D);
        }

        public IActor Actor { get; set; }
        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.gameObject.TryGetComponent(out Actor actor) && actor.TryGetHecsComponent(out PlayerTagComponent playerTagComponent)) 
            {
                DamageCommand damageCommand = new DamageCommand();
                damageCommand.amount = damageAmountComponent.amount;
                damageCommand.authorEntity = Owner;
                actor.Command(damageCommand);
            }
        }
    }
}