using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerAnimationSystem : BaseSystem, IFixedUpdatable, IHaveActor, IReactCommand<Commands.InputCommand>, IReactCommand<Commands.InputEndedCommand>, IReactCommand<Commands.DamageCommand>, IReactCommand<Commands.EventStateAnimationCommand>
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
            var commandHorizontalSpeed = new FloatAnimationCommand
            {
                Index = AnimParametersMap.HorizontalSpeed,
                Value = Math.Abs(currentSpeedComponent.speed.x / speedCoeffComponent.coefficient)
            };
            Owner.Command(commandHorizontalSpeed);
            
            var commandVerticalSpeed = new FloatAnimationCommand
            {
                Index = AnimParametersMap.VerticalSpeed,
                Value = currentSpeedComponent.speed.y
            };
            Owner.Command(commandVerticalSpeed);

            var commandIsJumping = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isJumping
            };
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
            if (command.Index != InputIdentifierMap.Hit) return;
            var commandIsAttacking = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isAttacking,
                Value = true
            };
            Owner.Command(commandIsAttacking);
        }
        
        public void CommandReact(DamageCommand command)
        {
            if (!command.authorEntity.Equals(Owner))
            {
                BoolAnimationCommand commandIsDamaging = new BoolAnimationCommand();
                commandIsDamaging.Index = AnimParametersMap.isDamaging;
                commandIsDamaging.Value = true;
                Owner.Command(commandIsDamaging);
            }
        }
        
        public IActor Actor { get; set; }
        public void CommandReact(InputEndedCommand command)
        {
            if (command.Index != InputIdentifierMap.Hit) return;
            var commandIsNotAttacking = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isAttacking,
                Value = false
            };
            Owner.Command(commandIsNotAttacking);
        }

        public void CommandReact(EventStateAnimationCommand command)
        {
            if (command.StateId != AnimatorStateIdentifierMap.Ellen_Hurt) return;
            var commandIsNotDamaging = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isDamaging,
                Value = false
            };
            Owner.Command(commandIsNotDamaging);
        }
    }
}