using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class RangeAttackComponent : BaseComponent
    {
        [field: SerializeField] public int AttackPower { get; private set; } = 1;
        [field: SerializeField] public float AttackRangeRadius { get; private set; } = 1;
        [field: SerializeField] public LayerMask EnemyLayerMask { get; private set; }
        [field: SerializeField] public GameObject WarheadPrefab { get; private set; }
        [field: SerializeField] public int WarheadsAmount { get; private set; } = 1;
    }
}