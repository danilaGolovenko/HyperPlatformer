using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using HECSFramework.Core.Helpers;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerHealthHolderComponent : BaseComponent, IHealthComponent, IAfterEntityInit
    {
        private PlayerHolderComponent playerHolderComponent;

        public ReactiveValue<int> currentHealth
        {
            get => playerHolderComponent.PlayerEntity.GetHealthComponent().currentHealth;
            set => playerHolderComponent.PlayerEntity.GetHealthComponent().currentHealth = value;
        }
        public void AfterEntityInit()
        {
            playerHolderComponent = Owner.GetPlayerHolderComponent();
        }
    }
}