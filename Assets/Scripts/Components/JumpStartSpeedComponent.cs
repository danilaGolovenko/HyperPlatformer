using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class JumpStartSpeedComponent : BaseComponent
    {
        [field: SerializeField] public float startSpeed { get; private set; } = 10;
    }
}