using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class CharacterContainerHolderComponent : BaseComponent
    {
        [field: SerializeField] public ActorContainer CharacterContainer { get; private set; }
    }
}