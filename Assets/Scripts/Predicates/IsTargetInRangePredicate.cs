using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using UnityEngine;

namespace Predicates
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class IsTargetInRangePredicate : IPredicate
    {
        public bool IsReady(IEntity target, IEntity owner = null)
        {
            var abilityOwnerPos = owner.GetAbilityOwnerComponent().AbilityOwner.GetUnityTransformComponent().Transform.transform.position;
            var targetPos = target.GetUnityTransformComponent().Transform.transform.position;
            var abilityRangeRadius = owner.GetAbilityRangeRadiusComponent().RangeRadius;
            return Vector3.Distance(abilityOwnerPos, targetPos) >= abilityRangeRadius;
        }
    }
}