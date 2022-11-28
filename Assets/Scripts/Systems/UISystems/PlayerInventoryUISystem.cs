using System;
using System.Collections.Generic;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerInventoryUISystem : BaseSystem, IHaveActor, IReactGlobalCommand<Commands.OnPickUpItemCommand>, ILateStart
    {
        private PlayerInventoryComponent playerInventoryComponent;
        private PlayerInventoryUIMonoComponent playerInventoryUIMonoComponent;
        private ItemsListComponent itemsListComponent;
        public override void InitSystem()
        {
            itemsListComponent = Owner.World.GetSingleComponent<ItemsListComponent>();
            Actor.TryGetComponent(out playerInventoryUIMonoComponent);
            IEntity player = Owner.World.GetSingleComponent<PlayerTagComponent>().Owner;
            player.TryGetHecsComponent(out playerInventoryComponent);
        }
        
        public void LateStart()
        {
            List<Item> inventory = new List<Item>();
            foreach (var dictionaryItem in playerInventoryComponent.currentItems)
            {
                Item item = new Item
                {
                    itemContainer = (ActorContainer)itemsListComponent.GetContainerByIndex(dictionaryItem.Key),
                    amount = dictionaryItem.Value
                };
                inventory.Add(item);
            }
            playerInventoryUIMonoComponent.InitInventoryUI(inventory);
        }
        
        public IActor Actor { get; set; }
        public void CommandGlobalReact(OnPickUpItemCommand command)
        {
            List<Item> inventory = new List<Item>();
            foreach (var dictionaryItem in playerInventoryComponent.currentItems)
            {
                Item item = new Item
                {
                    itemContainer = (ActorContainer)itemsListComponent.GetContainerByIndex(dictionaryItem.Key),
                    amount = dictionaryItem.Value
                };
                inventory.Add(item);
            }
            playerInventoryUIMonoComponent.UpdateInventoryUI(inventory);
        }
    }
}