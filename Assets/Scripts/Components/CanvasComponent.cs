using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class CanvasComponent : BaseComponent
    {
        [field: SerializeField] public GameObject canvas { get; private set; }
    }
}