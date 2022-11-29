using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Helpers;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class WarheadTouchSystem : BaseSystem, IFixedUpdatable, IHaveActor
    {
        [Required] private WarheadComponent warheadComponent;
        private Rigidbody2D rb;
        private float radius = 1f;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
        }
        
        private IActor GetTarget()
        {
            var hit = Physics2D.CircleCast(rb.position, radius, Vector2.zero, 0f,
                warheadComponent.EnemyLayerMask);
            if (hit.collider == null) return null;
            hit.collider.gameObject.TryGetComponent(out Actor target);
            return target;
        }
        
        private bool HitPlatform()
        {
            var hit = Physics2D.CircleCast(rb.position, radius, Vector2.zero, 0f,
                warheadComponent.PlatformLayerMask);
            return hit.collider != null;
        }

        public void FixedUpdateLocal()
        {
            var target = GetTarget();
            if (target != null)
            {
                var damageCommand = new DamageCommand
                {
                    amount = warheadComponent.AttackPower,
                    authorEntity = Owner
                };
                target.Command(damageCommand);
                var destroyEntityWorldCommand = new DestroyEntityWorldCommand
                {
                    Entity = Owner
                };
                Owner.World.Command(destroyEntityWorldCommand);
                return;
            }
            if (!HitPlatform()) return;
            var destroyEntityCommand = new DestroyEntityWorldCommand
            {
                Entity = Owner
            };
            Owner.World.Command(destroyEntityCommand);
        }

        public IActor Actor { get; set; }
        }
}