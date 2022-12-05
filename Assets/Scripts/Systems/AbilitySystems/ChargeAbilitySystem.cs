using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class ChargeAbilitySystem : BaseAbilitySystem 
    {
        public override void InitSystem()
        {
        }

        public override void Execute(IEntity owner = null, IEntity target = null, bool enable = true)
        {
            Debug.Log("ChargeAbilitySystem");
        }
    }
}