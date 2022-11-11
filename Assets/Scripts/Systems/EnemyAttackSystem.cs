using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class EnemyAttackSystem : BaseSystem, IHaveActor, IFixedUpdatable
    {
        [Required] private MeleeAttackComponent meleeAttackComponent;
        private Rigidbody2D rb;
        private float waitTime = 0.5f;
        private float currentWaitTime = 0;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
        }

        public IActor Actor { get; set; }
        public void FixedUpdateLocal()
        {
            if (currentWaitTime > 0)
            {
                currentWaitTime -= Time.fixedDeltaTime;
                return;
            }
            RaycastHit2D hit = Physics2D.CircleCast(rb.position, meleeAttackComponent.attackRangeRadius, Vector2.zero, 0f,
                meleeAttackComponent.enemyLayerMask);
            if (hit.collider != null)
            {
                DamageCommand damageCommand = new DamageCommand();
                damageCommand.amount = meleeAttackComponent.attackPower;
                damageCommand.authorEntity = Owner;
                hit.collider.gameObject.TryGetComponent(out Actor victim);
                victim.Command(damageCommand);
                currentWaitTime = waitTime;
            }
        }
    }
}