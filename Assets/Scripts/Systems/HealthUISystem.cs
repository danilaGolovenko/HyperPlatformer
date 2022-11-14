using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class HealthUISystem : BaseSystem
    {
        [Required] private EntityWithHealthComponent entityWithHealthComponent;
        [Required] private HealthIconPrefabComponent healthIconPrefabComponent;
        public override void InitSystem()
        {
            
        }
    }
}