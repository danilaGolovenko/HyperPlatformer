using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class AbilityRangeRadiusComponent : BaseComponent
    {
        [field: SerializeField] public int RangeRadius { get; private set; } 
    }
}