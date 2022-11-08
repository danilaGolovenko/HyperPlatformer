using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class CurrentSpeedComponent : BaseComponent
    {
        [field: SerializeField] public Vector2 speed = Vector2.zero;
    }
}