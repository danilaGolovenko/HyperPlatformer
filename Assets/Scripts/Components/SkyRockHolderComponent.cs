using System;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class SkyRockHolderComponent : BaseComponent
    {
        [field: SerializeField] public ActorContainer SkyRockHolderContainer { get; private set; }
    }
}