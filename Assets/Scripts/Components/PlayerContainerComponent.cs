using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerContainerComponent : BaseComponent
    {
        [field: SerializeField] public ActorContainer actorContainer { get; private set; }
    }
}