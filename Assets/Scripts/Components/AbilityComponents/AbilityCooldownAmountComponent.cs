using System;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class AbilityCooldownAmountComponent : BaseComponent
    {
        [field: SerializeField] public float TimeInSeconds { get; set; } 
    }
}