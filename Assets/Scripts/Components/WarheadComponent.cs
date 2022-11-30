using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class WarheadComponent : BaseComponent
    {
        public int AttackPower { get; set; } = 1;
        [field: SerializeField] public LayerMask EnemyLayerMask { get; private set; }
        [field: SerializeField] public LayerMask PlatformLayerMask { get; private set; }
    }
}