using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ChomperAnimationSystem : BaseSystem, IFixedUpdatable, IHaveActor,
        IReactCommand<Commands.DamageCommand>, IReactCommand<Commands.EventStateAnimationCommand>
    {
        [Required] private CurrentSpeedComponent currentSpeedComponent;
        private SpriteRenderer spriteRenderer;
        
        public override void InitSystem()
        {
            Owner.TryGetHecsComponent(HMasks.CurrentSpeedComponent, out currentSpeedComponent);
            Actor.TryGetComponent(out spriteRenderer);
        }

        private void UpdateFlipX()
        {
            if (currentSpeedComponent.speed.x != 0)
            {
                spriteRenderer.flipX = currentSpeedComponent.speed.x < 0;
            }
        }
        
        public void FixedUpdateLocal()
        {
            UpdateFlipX();
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
            if (command.StateId != AnimatorStateIdentifierMap.ChomperHit) return;
            var commandIsNotDamaging = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isDamaging,
                Value = false
            };
            Owner.Command(commandIsNotDamaging);
        }
    }
}