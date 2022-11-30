using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Strategies
{
    public class FlipToCorrectDirectionNode : InterDecision
    {
        public override string TitleOfNode { get; } = "FlipToCorrectDirection";
        protected override void Run(IEntity entity)
        {
            ((Actor)entity).TryGetComponent(out Rigidbody2D rb);
            ((Actor)entity).TryGetComponent(out SpriteRenderer spriteRenderer);
            var rangeAttackComponent = entity.GetRangeAttackComponent();
            var hit = Physics2D.CircleCast(rb.position, rangeAttackComponent.AttackRangeRadius, Vector2.zero, 0f, 
                rangeAttackComponent.EnemyLayerMask);
            if (hit.collider != null)
            {
                hit.collider.gameObject.TryGetComponent(out Actor target);
                spriteRenderer.flipX = !(target.GetUnityTransformComponent().Transform.transform.position.x >
                                         entity.GetUnityTransformComponent().Transform.transform.position.x);
            }
            Next.Execute(entity);
        }
    }
}