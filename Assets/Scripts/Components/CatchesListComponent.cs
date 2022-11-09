using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class CatchesListComponent : BaseComponent
    {
        [field: SerializeField] public List<Rigidbody2D> rbList { get; private set; } = new();
    }
}