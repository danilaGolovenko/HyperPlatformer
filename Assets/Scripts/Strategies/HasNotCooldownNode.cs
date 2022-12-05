using Components;
using HECSFramework.Core;

namespace Strategies
{
    public class HasNotCooldownNode : DilemmaDecision
    {
        public override string TitleOfNode { get; } = "HasNotCooldown";
        protected override void Run(IEntity entity)
        {
            if (entity.TryGetHecsComponent(out CooldownComponent cooldownComponent) && cooldownComponent.CurrentTime > 0)
            {
                Negative.Execute(entity);
            } 
            else
            {
                Positive.Execute(entity);
            }
        }
    }
}