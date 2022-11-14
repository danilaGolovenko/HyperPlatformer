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
    public sealed class SpitterAnimationSystem : BaseSystem, IFixedUpdatable, IHaveActor,
        IReactCommand<Commands.DamageCommand>, IReactCommand<Commands.EventStateAnimationCommand>
    {
        [Required] private CurrentSpeedComponent currentSpeedComponent;
        [Required] private MeleeAttackComponent meleeAttackComponent;
        private SpriteRenderer spriteRenderer;

        private Rigidbody2D rb;

        public override void InitSystem()
        {
            Owner.TryGetHecsComponent(HMasks.CurrentSpeedComponent, out currentSpeedComponent);
            Actor.TryGetComponent(out spriteRenderer);
            Actor.TryGetComponent(out rb);
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
            UpdateFlipX();
        }

        public IActor Actor { get; set; }

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

        public void CommandReact(EventStateAnimationCommand command)
        {
            if (command.StateId == AnimatorStateIdentifierMap.SpitterHit)
            {
                BoolAnimationCommand commandIsNotDamaging = new BoolAnimationCommand();
                commandIsNotDamaging.Index = AnimParametersMap.isDamaging;
                commandIsNotDamaging.Value = false;
                Owner.Command(commandIsNotDamaging);
            }
        }
    }
}