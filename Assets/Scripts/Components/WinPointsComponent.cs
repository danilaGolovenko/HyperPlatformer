using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using Commands;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class WinPointsComponent : BaseComponent
    {
        public float currentAmount;
        [field: SerializeField] public float requiredAmount { get; private set; }
    }
}