using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class WarheadHolderComponent : BaseComponent
    {
        [field: SerializeField] public ActorContainer WarheadHolderContainer { get; private set; }
    }
}