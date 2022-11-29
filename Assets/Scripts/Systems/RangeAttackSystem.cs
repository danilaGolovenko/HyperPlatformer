using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class RangeAttackSystem : BaseSystem, IReactCommand<Commands.AnimationEventCommand>, IFixedUpdatable, IHaveActor
    {
        [Required] private RangeAttackComponent rangeAttackComponent;
        [Required] private WarheadHolderComponent warheadHolderComponent;
        private Rigidbody2D rb;
        private float waitTime = 1f;
        private float currentWaitTime = 0;
        private float bu = 1f;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
        }
        private IActor GetTarget()
        {
            var hit = Physics2D.CircleCast(rb.position, rangeAttackComponent.AttackRangeRadius, Vector2.zero, 0f,
                rangeAttackComponent.EnemyLayerMask);
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
            var target = GetTarget();
            if (target != null)
            {
                commandIsAttacking.Value = true;
                currentWaitTime = waitTime;
            }
            else
                commandIsAttacking.Value = false;
            Owner.Command(commandIsAttacking);
        }

        public IActor Actor { get; set; }
        public async void CommandReact(AnimationEventCommand command)
        {
            if (command.Id == AnimationEventIdentifierMap.StartEnemyAttack)
                Owner.AddHecsComponent(new StopMovingComponent());
            if (command.Id == AnimationEventIdentifierMap.Shooting)
            {
                for (var i=0; i < rangeAttackComponent.WarheadsAmount; i++)
                {
                    var position = rb.transform.position;
                    position.y += bu;
                    var actor = await warheadHolderComponent.WarheadHolderContainer.GetActor(true, null,
                        rb.transform.position);
                    var warheadComponent = actor.GetOrAddComponent<WarheadComponent>();
                    warheadComponent.AttackPower = rangeAttackComponent.AttackPower;
                    actor.Init();
                }
                
            }
        }
    }
}