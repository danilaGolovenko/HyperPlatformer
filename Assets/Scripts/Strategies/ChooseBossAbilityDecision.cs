using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Strategies
{
    public class ChooseBossAbilityDecision : LogNode
    {
        [Connection(ConnectionPointType.In, "Input")] public BaseDecisionNode Input;
        [Connection(ConnectionPointType.Out, "Punch")] public BaseDecisionNode Punch;
        [Connection(ConnectionPointType.Out, "Charge")] public BaseDecisionNode Charge;
        [Connection(ConnectionPointType.Out, "Rockfall")] public BaseDecisionNode Rockfall;
        [Connection(ConnectionPointType.Out, "OutOfRange")] public BaseDecisionNode OutOfRange;
        public override string TitleOfNode { get; } = "ChooseBossAbility";
        private float circleCastDistance = 2f;
        
        
        protected override void Run(IEntity entity)
        {
            var bossComponent = entity.GetBossComponent(); 
            ((Actor)entity).TryGetComponent(out Rigidbody2D rb);

            var hit = Physics2D.CircleCast(rb.position, bossComponent.PunchRangeRadius, Vector2.up, circleCastDistance,
                bossComponent.EnemyLayerMask);
            if (hit.collider != null)
            {
                Punch.Execute(entity);
                return;
            }

            hit = Physics2D.CircleCast(rb.position, bossComponent.RockfallRangeRadius, Vector2.up, circleCastDistance,
                bossComponent.EnemyLayerMask);
            if (hit.collider != null)
            {
                Rockfall.Execute(entity);
                return;
            }
            
            hit = Physics2D.CircleCast(rb.position, bossComponent.ChargeRangeRadius, Vector2.up, circleCastDistance,
                bossComponent.EnemyLayerMask);
            if (hit.collider != null)
            {
                Charge.Execute(entity);
                return;
            }
            
            OutOfRange.Execute(entity);
        }
    }
}