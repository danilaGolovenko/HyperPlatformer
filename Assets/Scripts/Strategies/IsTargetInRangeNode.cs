using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Strategies
{
    public class IsTargetInRangeNode : DilemmaDecision
    {
        public override string TitleOfNode { get; } = "IsTargetInRange";
        protected override void Run(IEntity entity)
        {
            ((Actor)entity).TryGetComponent(out Rigidbody2D rb);
            var rangeAttackComponent = entity.GetRangeAttackComponent();

            var hit = Physics2D.CircleCast(rb.position, rangeAttackComponent.AttackRangeRadius, Vector2.zero, 0f,
                rangeAttackComponent.EnemyLayerMask);
            if (hit.collider == null)
                Negative.Execute(entity);
            else
                Positive.Execute(entity);
        }
    }
}


