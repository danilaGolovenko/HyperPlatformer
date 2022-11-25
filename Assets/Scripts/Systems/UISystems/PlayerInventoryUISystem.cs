using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerInventoryUISystem : BaseSystem, IHaveActor
    {
        private PlayerInventoryUIMonoComponent playerInventoryUIMonoComponent;
        private ItemsListComponent itemsListComponent;
        public override void InitSystem()
        {
            itemsListComponent = Owner.World.GetSingleComponent<ItemsListComponent>();
            Actor.TryGetComponent(out playerInventoryUIMonoComponent);
            // todo взять инвентарь игрока, преобразовать его при помощи itemsListComponent в список Item'ов
            // playerInventoryUIMonoComponent.InitInventoryUI(...);
        }
        // todo обновление UI при обновлении инвентаря
        public IActor Actor { get; set; }
    }
}