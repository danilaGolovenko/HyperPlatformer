using Commands;
using HECSFramework.Core;

namespace Strategies
{
    public class SendBoolAnimationCommandIsAttackingNode : InterDecision
    {
        public override string TitleOfNode { get; } = "SendBoolAnimationCommandIsAttacking";
        protected override void Run(IEntity entity)
        {
            var commandIsAttacking = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isAttacking,
                Value = true
            };
            entity.Command(commandIsAttacking);
            Next.Execute(entity);
        }
    }
}