using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class CooldownProcessingSystem : BaseSystem, IFixedUpdatable
    {
        public override void InitSystem()
        {
        }

        public void FixedUpdateLocal() 
        {
            var entities = Owner.World.Filter(HMasks.CooldownComponent).Data;
            foreach (var entity in entities)
            {
                if (entity == null) continue;
                var cooldown = entity.GetCooldownComponent();
                if (cooldown.CurrentTime > 0)
                    entity.GetCooldownComponent().CurrentTime -= Time.fixedDeltaTime;
            }
        }
    }
}