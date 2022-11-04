using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class JumpingComponent : BaseComponent
    {
        [field: SerializeField]
        public LayerMask layerMask { get; private set; }
    }
}