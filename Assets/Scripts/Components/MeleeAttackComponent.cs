using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class MeleeAttackComponent : BaseComponent
    {
        [field: SerializeField] public float attackPower { get; private set; } = 1;
        [field: SerializeField] public float attackRangeRadius { get; private set; } = 1;
        [field: SerializeField] public LayerMask enemyLayerMask { get; private set; }
    }
}