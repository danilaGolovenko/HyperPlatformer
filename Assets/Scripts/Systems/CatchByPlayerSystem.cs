using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class CatchByPlayerSystem : BaseSystem, IReactCommand<Trigger2dEnterCommand>
    {
        public override void InitSystem()
        {
        }

        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.gameObject.TryGetComponent(out Actor actor) && 
                actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent)){
                playerHolderComponent.PlayerEntity.GetPlayerKeysAmountComponent().amount.CurrentValue++;

                DestroyEntityWorldCommand destroyEntityWorldCommand = new DestroyEntityWorldCommand();
                destroyEntityWorldCommand.Entity = Owner;
                Owner.World.Command(destroyEntityWorldCommand);
            }
        }
    }
}