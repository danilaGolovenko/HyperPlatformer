using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class GravitySystem : BaseSystem, IFixedUpdatable, IReactComponentGlobal<GroundedTagComponent>
    {
        private ConcurrencyList<IEntity> entities;
        public override void InitSystem()
        {
            entities = Owner.World.Filter(new FilterMask(HMasks.SubjectToGravityTagComponent, HMasks.CurrentSpeedComponent), new FilterMask(HMasks.GroundedTagComponent));
        }

        public void FixedUpdateLocal()
        {
            foreach (var entity in entities)
            {
                float speedX = entity.GetCurrentSpeedComponent().speed.x;
                float speedY = entity.GetCurrentSpeedComponent().speed.y -= 9.8f * Time.fixedDeltaTime;
                entity.GetCurrentSpeedComponent().speed = new Vector2(speedX, speedY);
            }
        }

        public void ComponentReactGlobal(GroundedTagComponent component, bool isAdded)
        {
            if (isAdded)
            {
                float speedX = component.Owner.GetCurrentSpeedComponent().speed.x;
                float speedY = 0f;
                component.Owner.GetCurrentSpeedComponent().speed = new Vector2(speedX, speedY);
            }
        }
    }
}