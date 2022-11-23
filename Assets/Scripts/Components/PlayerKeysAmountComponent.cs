using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using HECSFramework.Core.Helpers;
using UnityEngine;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerKeysAmountComponent : BaseComponent
    {
        [SerializeField] public ReactiveValue<int> amount = new ReactiveValue<int>(0);
    }
}