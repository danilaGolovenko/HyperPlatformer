using System;
using System.Collections.Generic;
using Commands;
using HECSFramework.Core;
using Components;
using HECSFramework.Unity;
using UnityEngine;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerMeleeAttackSystem : BaseSystem, IReactCommand<Commands.InputStartedCommand>, IHaveActor
    {
        [Required] private MeleeAttackComponent meleeAttackComponent;
        private Rigidbody2D rb;
        private ConcurrencyList<IEntity> enemies;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
            enemies = Owner.World.Filter(HMasks.EnemyTagComponent);
        }

        public void CommandReact(InputStartedCommand command)
        {
            if (command.Index == InputIdentifierMap.Hit) 
            {
                RaycastHit2D hit = Physics2D.CircleCast(rb.position, meleeAttackComponent.attackRangeRadius, Vector2.zero, 0f,
                    meleeAttackComponent.enemyLayerMask);
                if (hit.collider != null)
                {
                    DamageCommand damageCommand = new DamageCommand();
                    damageCommand.amount = meleeAttackComponent.attackPower;
                    damageCommand.authorEntity = Owner;
                    hit.collider.gameObject.TryGetComponent(out Actor victim);
                    victim.Command(damageCommand);
                }
            }
        }

        public IActor Actor { get; set; }
    }
}