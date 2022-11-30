using Commands;
using HECSFramework.Core;

namespace Strategies
{
    public class SendBoolAnimationCommandIsNotAttackingNode : InterDecision
    {
        public override string TitleOfNode { get; } = "SendBoolAnimationCommandIsNotAttacking";
        protected override void Run(IEntity entity)
        {
            var commandIsNotAttacking = new BoolAnimationCommand
            {
                Index = AnimParametersMap.isAttacking,
                Value = false
            };
            entity.Command(commandIsNotAttacking);
            Next.Execute(entity);
        }
    }
}