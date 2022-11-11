using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class SpitterAnimationSystem : BaseSystem, IFixedUpdatable, IHaveActor, IReactCommand<Commands.DamageCommand>
    {
        [Required] private CurrentSpeedComponent currentSpeedComponent;
        [Required] private MeleeAttackComponent meleeAttackComponent;
        private SpriteRenderer spriteRenderer;
        private float waitingTime = 3;
        private float currentWaitingTime = 0;
        private Rigidbody2D rb;
        public override void InitSystem()
        {
            Owner.TryGetHecsComponent(HMasks.CurrentSpeedComponent, out currentSpeedComponent);
            // Owner.TryGetHecsComponent(HMasks.SpeedCoeffComponent, out speedCoeffComponent);
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
            if (currentWaitingTime > 0)
            {
                currentWaitingTime--;
                if (currentWaitingTime == 0)
                {
                    BoolAnimationCommand commandIsDamaging = new BoolAnimationCommand();
                    commandIsDamaging.Index = AnimParametersMap.isDamaging;
                    commandIsDamaging.Value = false;
                    Owner.Command(commandIsDamaging);
                }
            }
            
            RaycastHit2D hit = Physics2D.CircleCast(rb.position, meleeAttackComponent.attackRangeRadius, Vector2.zero, 0f, meleeAttackComponent.enemyLayerMask);
            BoolAnimationCommand commandIsAttaking = new BoolAnimationCommand();
            commandIsAttaking.Index = AnimParametersMap.isAttacking;
            commandIsAttaking.Value = (hit.collider != null);
            Owner.Command(commandIsAttaking);
            
            UpdateFlipX();
        }

        public IActor Actor { get; set; }
        public void CommandReact(DamageCommand command)
        {
            if (command.authorEntity.Equals(Owner))
            {
                BoolAnimationCommand commandIsDamaging = new BoolAnimationCommand();
                commandIsDamaging.Index = AnimParametersMap.isDamaging;
                commandIsDamaging.Value = true;
                Owner.Command(commandIsDamaging);
                currentWaitingTime = waitingTime;
            }
        }
    }
}