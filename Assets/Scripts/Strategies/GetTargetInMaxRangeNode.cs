using Commands;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using Helpers;
using UnityEngine;

namespace Strategies
{
    public sealed class GetTargetInMaxRangeNode : GenericNode<IEntity>
    {
        public override string TitleOfNode { get; } = "GetTargetInMaxRange";
        
        [Connection(ConnectionPointType.Out, "<IEntity> Out")]
        public BaseDecisionNode Out;
        public override IEntity Value(IEntity entity)
        {
            return entity.TryGetHecsComponent(out TargetHolderComponent targetHolderComponent) ? targetHolderComponent.entity : null;
        }

        public override void Execute(IEntity entity)
        {
        }
    }
}