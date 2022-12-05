using HECSFramework.Core;
using HECSFramework.Unity;
using System;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class CooldownComponent : BaseComponent
    {
        public float CurrentTime { get; set; } 
    }
}