using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class EnemyAttackSystem : BaseSystem, IHaveActor, IFixedUpdatable, IReactCommand<Commands.AnimationEventCommand>
    {
        [Required] private MeleeAttackComponent meleeAttackComponent;
        private Rigidbody2D rb;
        private float waitTime = 1f;
        private float currentWaitTime = 0;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
        }

        public IActor Actor { get; set; }

        private IActor GetTarget()
        {
            var hit = Physics2D.CircleCast(rb.position, meleeAttackComponent.attackRangeRadius, Vector2.zero, 0f,
                meleeAttackComponent.enemyLayerMask);
            if (hit.collider == null) return null;
            hit.collider.gameObject.TryGetComponent(out Actor target);
            return target;
        }
        
        public void FixedUpdateLocal()
        {
            if (currentWaitTime > 0)
            {
                currentWaitTime -= Time.fixedDeltaTime;
                return;
            }

            var commandIsAttacking = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isAttacking
            };
            var victim = GetTarget();
            if (victim != null)
            {
                commandIsAttacking.Value = true;
                currentWaitTime = waitTime;
            }
            else
                commandIsAttacking.Value = false;
            Owner.Command(commandIsAttacking);
        }

        public void CommandReact(AnimationEventCommand command)
        {
            // todo переписать на SendStateAnimationEvent?
            if (command.Id == AnimationEventIdentifierMap.StartEnemyAttack)
                Owner.AddHecsComponent(new StopMovingComponent());
            if (command.Id == AnimationEventIdentifierMap.Shooting)
            {
                var damageCommand = new DamageCommand
                {
                    amount = meleeAttackComponent.attackPower,
                    authorEntity = Owner
                };
                var victim = GetTarget();
                if (victim != null)
                    GetTarget().Command(damageCommand);
            }
        }
    }
}