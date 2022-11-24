using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using HECSFramework.Core.Helpers;
using UnityEngine;

namespace Components
{
    public interface IHealthComponent
    {
        public ReactiveValue<int> currentHealth { get; set; }
    }
    
    
    [Serializable][Documentation(Doc.NONE, "")]
    public class HealthComponent : BaseComponent, IInitable, IHealthComponent
    {
        [field: SerializeField] public int maxHealth { get; set; } = 7;
        [SerializeField] public ReactiveValue<int> currentHealth { get; set; } = new ReactiveValue<int>(7);
        
        public void Init()
        {
            currentHealth.CurrentValue = maxHealth;
        }
    }
}  