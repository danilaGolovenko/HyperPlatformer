using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class RequiredAmountOfKeysComponent : BaseComponent
    {
       [field: SerializeField] public int amount { get; private set; }
    }
}