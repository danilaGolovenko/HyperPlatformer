using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class InventoryInitializationSystem : BaseSystem, IAfterEntityInit
    {
        [Required] private StartInventoryComponent startInventoryComponent;
        [Required] private PlayerInventoryComponent playerInventoryComponent;
        public override void InitSystem()
        {
        }

        public void AfterEntityInit()
        {
            foreach (var item in startInventoryComponent.startItemsList)
            {
                playerInventoryComponent.currentItems.Add(item.itemContainer.ContainerIndex, item.amount);
            }
        }
    }
}