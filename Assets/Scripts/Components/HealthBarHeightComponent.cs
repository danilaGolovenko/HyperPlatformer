using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class HealthBarHeightComponent : BaseComponent
    {
        [field: SerializeField] public float height { get; private set; } = 0;
    }
}