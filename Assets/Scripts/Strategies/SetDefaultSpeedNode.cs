using HECSFramework.Core;

namespace Strategies
{
    public class SetDefaultSpeedNode : InterDecision
    {
        public override string TitleOfNode { get; } = "SetDefaultSpeed";
        protected override void Run(IEntity entity)
        {
            var speedCoeffComponent = entity.GetSpeedCoeffComponent();
            speedCoeffComponent.coefficient = speedCoeffComponent.defaultCoefficient;
            Next.Execute(entity);
        }
    }
}