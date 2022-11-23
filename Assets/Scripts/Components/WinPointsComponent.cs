using HECSFramework.Core;
using System;
using HECSFramework.Core.Helpers;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class WinPointsComponent : BaseComponent
    {
        public ReactiveValue<int> currentAmount = new ReactiveValue<int>(0);
        [field: SerializeField] public int requiredAmount { get; set; }

        public override string ToString()
        {
            return currentAmount.CurrentValue + "/" + requiredAmount;
        }
    }
}