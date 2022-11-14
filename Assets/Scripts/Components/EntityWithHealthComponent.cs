using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class EntityWithHealthComponent : BaseComponent
    {
        [field: SerializeField] public IEntity entity {get; private set;}
        [field: SerializeField] public HealthComponent healthComponent { get; private set; }
    }
}