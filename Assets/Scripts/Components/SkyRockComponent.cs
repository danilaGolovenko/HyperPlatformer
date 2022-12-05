using System;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class SkyRockComponent : BaseComponent
    {
        [field: SerializeField] public int AttackPower { get; set; } = 1;
        [field: SerializeField] public LayerMask EnemyLayerMask { get; private set; }
        [field: SerializeField] public LayerMask PlatformLayerMask { get; private set; }
    }
}