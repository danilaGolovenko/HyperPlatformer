using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerAnimationSystem : BaseSystem, IFixedUpdatable, IHaveActor, IReactCommand<Commands.InputCommand>, IReactCommand<Commands.InputEndedCommand>
    {
        [Required] private CurrentSpeedComponent currentSpeedComponent;
        [Required] private SpeedCoeffComponent speedCoeffComponent;
        private SpriteRenderer spriteRenderer;
        public override void InitSystem()
        {
            Owner.TryGetHecsComponent(HMasks.CurrentSpeedComponent, out currentSpeedComponent);
            Owner.TryGetHecsComponent(HMasks.SpeedCoeffComponent, out speedCoeffComponent);
            Actor.TryGetComponent(out spriteRenderer);
        }

        private void UpdateFlipX()
        {
            if (currentSpeedComponent.speed.x != 0)
            {
                if (currentSpeedComponent.speed.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
        }

        public void FixedUpdateLocal()
        {
            FloatAnimationCommand commandHorizontalSpeed = new FloatAnimationCommand();
            commandHorizontalSpeed.Index = AnimParametersMap.HorizontalSpeed;
            commandHorizontalSpeed.Value = Math.Abs(currentSpeedComponent.speed.x / speedCoeffComponent.coefficient);
            Owner.Command(commandHorizontalSpeed);
            
            FloatAnimationCommand commandVerticalSpeed = new FloatAnimationCommand();
            commandVerticalSpeed.Index = AnimParametersMap.VerticalSpeed;
            commandVerticalSpeed.Value = currentSpeedComponent.speed.y;
            Owner.Command(commandVerticalSpeed);

            BoolAnimationCommand commandIsJumping = new BoolAnimationCommand();
            commandIsJumping.Index = AnimParametersMap.isJumping;
            if (Owner.TryGetHecsComponent(HMasks.GroundedTagComponent, out GroundedTagComponent groundedTag))
            {
                commandIsJumping.Value = false;
            }
            else 
            {
                commandIsJumping.Value = true ;
            }
            Owner.Command(commandIsJumping);
            UpdateFlipX();
        }

        public void CommandReact(InputCommand command)
        {
            if (command.Index == InputIdentifierMap.Hit)
            {
                BoolAnimationCommand commandIsAttacking = new BoolAnimationCommand();
                commandIsAttacking.Index = AnimParametersMap.isAttacking;
                commandIsAttacking.Value = true;
                Owner.Command(commandIsAttacking);
            }
        }

        public IActor Actor { get; set; }
        public void CommandReact(InputEndedCommand command)
        {
            if (command.Index == InputIdentifierMap.Hit)
            {
                BoolAnimationCommand commandIsNotAttacking = new BoolAnimationCommand();
                commandIsNotAttacking.Index = AnimParametersMap.isAttacking;
                commandIsNotAttacking.Value = false;
                Owner.Command(commandIsNotAttacking);
            }
        }
    }
}