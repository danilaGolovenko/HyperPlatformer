using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class BossAnimationSystem : BaseSystem, IFixedUpdatable, IHaveActor,
    IReactCommand<Commands.DamageCommand>, IReactCommand<Commands.EventStateAnimationCommand>
    {
        [Required] private CurrentSpeedComponent currentSpeedComponent;
        [Required] private SpeedCoeffComponent speedCoeffComponent;
        private SpriteRenderer spriteRenderer;
        private float timeOfDeathAnimation = 8f;
        
        public override void InitSystem()
        {
            Owner.TryGetHecsComponent(HMasks.CurrentSpeedComponent, out currentSpeedComponent);
            Actor.TryGetComponent(out spriteRenderer);
        }

        private void UpdateFlipX()
        {
            if (currentSpeedComponent.speed.x != 0)
            {
                spriteRenderer.flipX = currentSpeedComponent.speed.x > 0;
            }
        }
        
        private void UpdateHorizontalSpeed()
        {
            var commandHorizontalSpeed = new FloatAnimationCommand
            {
                Index = AnimParametersMap.HorizontalSpeed,
                Value = Math.Abs(currentSpeedComponent.speed.x  / speedCoeffComponent.coefficient)
            };
            Owner.Command(commandHorizontalSpeed);
        }
        public void FixedUpdateLocal()
        {
            UpdateFlipX();
            UpdateHorizontalSpeed();
        }

        public IActor Actor { get; set; }

        public void CommandReact(DamageCommand command)
        {
            if (command.authorEntity.Equals(Owner)) return;
            var commandIsDamaging = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isDamaging,
                Value = true
            };
            Owner.Command(commandIsDamaging);
        }

        public void CommandReact(EventStateAnimationCommand command)
        {
            if (command.StateId == AnimatorStateIdentifierMap.Boss_PunchAttack)
            {
                var commandIsNotAttacking = new BoolAnimationCommand
                {
                    Index = AnimParametersMap.isPunchAttacking,
                    Value = false
                };
                Owner.Command(commandIsNotAttacking);
            }
            if (command.StateId == AnimatorStateIdentifierMap.Boss_Death && command.AnimationId == AnimationEventIdentifierMap.StartClip)
            {
                var cooldown = Owner.GetOrAddComponent<CooldownComponent>();
                cooldown.CurrentTime = timeOfDeathAnimation;
            }

            if (command.StateId == AnimatorStateIdentifierMap.Boss_Rockfall)
            {
                if (command.AnimationId == AnimationEventIdentifierMap.Shooting)
                {
                    Owner.AddHecsSystem(new SpawnSkyRocksSystem());
                }
                if (command.AnimationId == AnimationEventIdentifierMap.EndClip)
                {
                    Owner.RemoveHecsSystem<SpawnSkyRocksSystem>();
                }
            }

            // if (command.StateId != AnimatorStateIdentifierMap.ChomperHit) return;
            // var commandIsNotDamaging = new BoolAnimationCommand
            // {
            //     Index = AnimParametersMap.isDamaging,
            //     Value = false
            // };
            // Owner.Command(commandIsNotDamaging);
        }
    }
}