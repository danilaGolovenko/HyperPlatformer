using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using System;

namespace Predicates
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class HasNoCooldownPredicate : IPredicate
    {
        public bool IsReady(IEntity target, IEntity owner = null)
        {
            return !target.TryGetHecsComponent(HMasks.CooldownComponent, out CooldownComponent cooldown) || cooldown.CurrentTime <= 0;
        }
    }
}