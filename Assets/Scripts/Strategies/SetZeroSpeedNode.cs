using HECSFramework.Core;
using Unity.VisualScripting;

namespace Strategies
{
    public class SetZeroSpeedNode : InterDecision
    {
        public override string TitleOfNode { get; } = "SetZeroSpeed";
        protected override void Run(IEntity entity)
        {
            var speedCoeffComponent = entity.GetSpeedCoeffComponent();
            speedCoeffComponent.coefficient = 0f;
            Next.Execute(entity);
        }
    }
}