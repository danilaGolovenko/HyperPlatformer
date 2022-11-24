using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Unity.VisualScripting;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PickUpSystem : BaseSystem, IReactCommand<Trigger2dEnterCommand>, IAfterEntityInit
    {
        [Required] private PlayerHolderComponent playerHolderComponent;
        private PlayerInventoryComponent playerInventoryComponent;
        
        public override void InitSystem()
        {
        }
        
        public void AfterEntityInit()
        {
            playerInventoryComponent = playerHolderComponent.PlayerEntity.GetPlayerInventoryComponent();
        }
        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.gameObject.TryGetComponent(out Actor actor) &&
                actor.TryGetHecsComponent(out ItemTagComponent itemTag))
            {
                int itemContainerIndex = actor.GetActorContainerID().ContainerIndex;
                if(playerInventoryComponent.currentItems.ContainsKey(itemContainerIndex)){
                    playerInventoryComponent.currentItems[itemContainerIndex]++;
                }
                else
                {
                    playerInventoryComponent.currentItems.Add(itemContainerIndex, 1);
                }
                Owner.World.Command(new OnPickUpItemCommand());
                DestroyEntityWorldCommand destroyEntityWorldCommand = new DestroyEntityWorldCommand
                {
                    Entity = actor
                };
                Owner.World.Command(destroyEntityWorldCommand);
                Debug.Log("You took the key with index " + itemContainerIndex + ", теперь их целых " + playerInventoryComponent.currentItems[itemContainerIndex]);
            }
        }
        
    }
}