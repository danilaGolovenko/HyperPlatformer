using Commands;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using Helpers;
using UnityEngine;

namespace Systems 
{
    public class JumpingSystem : BaseSystem, IReactCommand<Commands.InputStartedCommand>, IHaveActor
    {
        [Required] private JumpStartSpeedComponent jumpStartSpeedComponent; 
        [Required] private CurrentSpeedComponent currentSpeedComponent;

        public override void InitSystem()
        {
        }
        public void CommandReact(InputStartedCommand command)
        {
            if (command.Index == InputIdentifierMap.Jump && Owner.ContainsMask<GroundedTagComponent>())
            {
                var newSpeed = new Vector2(currentSpeedComponent.speed.x, jumpStartSpeedComponent.startSpeed);
                currentSpeedComponent.speed = newSpeed;
            }
        }

        public IActor Actor { get; set; }
    }
}