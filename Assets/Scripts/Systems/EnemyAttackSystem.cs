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

        private IActor GetVictim()
        {
            RaycastHit2D hit = Physics2D.CircleCast(rb.position, meleeAttackComponent.attackRangeRadius, Vector2.zero, 0f,
                meleeAttackComponent.enemyLayerMask);
            if (hit.collider != null)
            {
                hit.collider.gameObject.TryGetComponent(out Actor victim);
                return victim;
            }
            return null;
        }
        
        public void FixedUpdateLocal()
        {
            if (currentWaitTime > 0)
            {
                currentWaitTime -= Time.fixedDeltaTime;
                return;
            }

            BoolAnimationCommand commandIsAttaking = new BoolAnimationCommand();
            commandIsAttaking.Index = AnimParametersMap.isAttacking;
            IActor victim = GetVictim();
            if (victim != null)
            {
                commandIsAttaking.Value = true;
                currentWaitTime = waitTime;
            }
            else
                commandIsAttaking.Value = false;
            Owner.Command(commandIsAttaking);
            
        }

        public void CommandReact(AnimationEventCommand command)
        {
            if (command.Id == AnimationEventIdentifierMap.Shooting)
            {
                DamageCommand damageCommand = new DamageCommand();
                damageCommand.amount = meleeAttackComponent.attackPower;
                damageCommand.authorEntity = Owner;
                var victim = GetVictim();
                if (victim != null)
                    GetVictim().Command(damageCommand);
            }
        }
    }
}