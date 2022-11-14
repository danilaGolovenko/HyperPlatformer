using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class StopMovingComponent : BaseComponent
    {
        [field: SerializeField] public float waitTIme { get; private set; } = 1f;
        public float currentWaitTime;
    }
}