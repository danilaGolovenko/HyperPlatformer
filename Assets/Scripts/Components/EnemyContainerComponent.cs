using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class EnemyContainerComponent : BaseComponent
    {
        [field: SerializeField] public ActorContainer ActorContainer { get; private set; }
    }
}