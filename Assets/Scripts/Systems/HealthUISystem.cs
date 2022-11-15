using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using Components;
using Components.MonoBehaviourComponents;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class HealthUISystem : BaseSystem, IHaveActor, ILateStart
    {
        private HealthUIMonoComponent healthUIMonoComponent;
        private IEntity player;

        public override void InitSystem()
        {
            Actor.TryGetComponent(out healthUIMonoComponent);
        }
        
        public IActor Actor { get; set; }
        
        public void LateStart()
        {
            player = Owner.World.GetSingleComponent<PlayerTagComponent>().Owner;
            player.TryGetHecsComponent(out HealthComponent healthComponent);
            healthComponent.currentHealth.OnChange += CurrentHealthOnChange;
            healthUIMonoComponent.InitHealthUI(healthComponent);
        }

        private void CurrentHealthOnChange(int obj)
        {
            healthUIMonoComponent.UpdateHealthUI();
        }
    }
}