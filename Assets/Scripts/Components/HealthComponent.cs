using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public class HealthComponent : BaseComponent, IInitable
    {
        [field: SerializeField] public float maxHealth { get; private set; } = 7;
        [SerializeField] public float currentHealth = 7;
        
        public void Init()
        {
            currentHealth = maxHealth;
        }
    }
}  