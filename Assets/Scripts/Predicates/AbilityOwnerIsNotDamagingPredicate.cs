using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using System;

namespace Predicates
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class AbilityOwnerIsNotDamagingPredicate : IPredicate
    {
        public bool IsReady(IEntity target, IEntity owner = null)
        {
            return !owner.GetAbilityOwnerComponent().AbilityOwner.GetBossComponent().IsDamaging;
        }
    }
}