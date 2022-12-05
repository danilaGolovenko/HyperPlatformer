using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using Helpers;
using UnityEngine;

namespace Strategies
{
    public sealed class PickUpTargetInMaxRangeNode : InterDecision
    {
        public override string TitleOfNode { get; } = "PickUpTargetInMaxRange";
        protected override void Run(IEntity entity)
        {
            ((Actor)entity).TryGetComponent(out Rigidbody2D rb);
            // ((Actor)entity).TryGetComponent(out BoxCollider2D boxCollider2D);
            // var boxColliderSize = boxCollider2D.size;
            // var size = new Vector2(boxColliderSize.y, boxColliderSize.y);
            // var bossComponent = entity.GetBossComponent();
            // var currentSpeedComponent = entity.GetCurrentSpeedComponent();
            // var dir = currentSpeedComponent.speed.x > 0 ? Vector2.right : Vector2.left;
            // var distance = boxColliderSize.x;
            // var hit = Physics2D.BoxCast(rb.position, size, 0f, dir,distance, bossComponent.EnemyLayerMask);

            var bossComponent = entity.GetBossComponent();
            var maxRange = bossComponent.PunchRangeRadius;
            if (maxRange < bossComponent.ChargeRangeRadius)
            {
                maxRange = bossComponent.ChargeRangeRadius;
            }
            if (maxRange < bossComponent.RockfallRangeRadius)
            {
                maxRange = bossComponent.RockfallRangeRadius;
            }
            var hit = Physics2D.CircleCast(rb.position, maxRange, Vector2.zero, 0f,
                bossComponent.EnemyLayerMask);
            if (hit.collider == null || !hit.collider.TryGetActorFromCollision(out var actor) ||
                !actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent))
            {
                if (entity.TryGetHecsComponent(out TargetHolderComponent targetHolderComponent))
                {
                    entity.RemoveHecsComponent<TargetHolderComponent>();
                }
            }
            else
            {
                entity.AddHecsComponent(new TargetHolderComponent() { entity = actor });
            }
            Next.Execute(entity);
        }
    }
}