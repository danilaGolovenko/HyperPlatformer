using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Helpers;
using Unity.VisualScripting;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PickUpSystem : BaseSystem, IReactCommand<Trigger2dEnterCommand>, IAfterEntityInit
    {
        [Required] private PlayerHolderComponent playerHolderComponent;
        private PlayerInventoryComponent playerInventoryComponent;
        private ItemsOnSceneComponent itemsOnSceneComponent;
        
        public override void InitSystem()
        {
            itemsOnSceneComponent = Owner.World.GetSingleComponent<ItemsOnSceneComponent>();
        }
        
        public void AfterEntityInit()
        {
            playerInventoryComponent = playerHolderComponent.PlayerEntity.GetPlayerInventoryComponent();
        }
        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (!command.Collider.TryGetActorFromCollision(out var actor) ||
                !actor.TryGetHecsComponent(out ItemTagComponent itemTag)) return;
            var itemContainerIndex = actor.GetActorContainerID().ContainerIndex;
            if(playerInventoryComponent.currentItems.ContainsKey(itemContainerIndex)){
                playerInventoryComponent.currentItems[itemContainerIndex]++;
            }
            else
            {
                playerInventoryComponent.currentItems.Add(itemContainerIndex, 1);
            }

            var currentSceneId = Owner.World.GetSingleComponent<CurrentSceneIdentifierComponent>().SceneId;
            var position = actor.GetUnityTransformComponent().Transform.transform.position;
            var entityContainer = ((Actor)actor).ActorContainer;
                
            var newItemsOnSceneList = itemsOnSceneComponent.ItemsOnScene.Where(itemOnScene => itemOnScene.SceneId != currentSceneId || itemOnScene.Position != position || itemOnScene.EntityContainer != entityContainer).ToList();
            itemsOnSceneComponent.ItemsOnScene = newItemsOnSceneList;
                
            var onPickUpItemCommand = new OnPickUpItemCommand
            {
                Item = actor
            };
            Owner.World.Command(onPickUpItemCommand);
                
            var commandIsPickedUp = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isPickUped,
                Value = true
            };
            actor.Command(commandIsPickedUp);
                
            actor.RemoveHecsComponent(itemTag);
        }
    }
}