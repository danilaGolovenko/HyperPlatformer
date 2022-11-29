using HECSFramework.Core;
using HECSFramework.Unity;
using System;
using Components.MonoBehaviourComponents;

namespace Components
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class WaitComponent : BaseComponent, IHaveActor, IInitable
    {
        public IActor Actor { get; set; }
        public WaitMonoComponent waitMonoComponent;
        
        public void Init()
        {
            if (Actor != null)
                waitMonoComponent = Actor.GameObject.GetComponent<WaitMonoComponent>();
        }
    }
}