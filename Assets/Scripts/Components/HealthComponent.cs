using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using HECSFramework.Core.Helpers;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public class HealthComponent : BaseComponent, IInitable
    {
        [field: SerializeField] public int maxHealth { get; set; } = 7;
        [SerializeField] public ReactiveValue<int> currentHealth = new ReactiveValue<int>(7);
        
        public void Init()
        {
            currentHealth.CurrentValue = maxHealth;
        }
    }
}  