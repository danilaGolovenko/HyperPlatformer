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
                
                OnPickUpItemCommand onPickUpItemCommand = new OnPickUpItemCommand();
                onPickUpItemCommand.Item = actor;
                Owner.World.Command(onPickUpItemCommand);

                BoolAnimationCommand commandIsPickUped = new BoolAnimationCommand();
                commandIsPickUped.Index = AnimParametersMap.isPickUped;
                commandIsPickUped.Value = true;
                actor.Command(commandIsPickUped);
                
                actor.RemoveHecsComponent(itemTag);
            }
        }
    }
}