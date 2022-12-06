using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class ChargeAbilityComponent : BaseComponent
    {
        [field: SerializeField] public int AttackPower { get; private set; }
        [field: SerializeField] public float ChargeSpeed { get; set; }
        [field: SerializeField] public float AttackRangeRadius { get; private set; }
    }
}