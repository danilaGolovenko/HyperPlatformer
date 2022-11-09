using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerAnimationSystem : BaseSystem, IFixedUpdatable
    {
        [Required] private CurrentSpeedComponent currentSpeedComponent;
        public override void InitSystem()
        {
            Owner.TryGetHecsComponent(HMasks.CurrentSpeedComponent, out currentSpeedComponent);
        }

        public void FixedUpdateLocal()
        {
            FloatAnimationCommand command = new FloatAnimationCommand();
            command.Index = AnimParametersMap.HorizontalSpeed;
            command.Value = currentSpeedComponent.speed.x;
            Owner.Command(command);
        }
    }
}