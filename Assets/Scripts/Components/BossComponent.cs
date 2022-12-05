using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class BossComponent : BaseComponent
    {
        [field: SerializeField] public LayerMask EnemyLayerMask { get; private set; }
        [field: SerializeField] public float PunchRangeRadius { get; private set; }
        [field: SerializeField] public float ChargeRangeRadius { get; private set; }
        [field: SerializeField] public float RockfallRangeRadius { get; private set; }
        public bool IsDamaging { get; set; } = false;
    }
}