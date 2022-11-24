using System;
using Commands;
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

        public override void InitSystem()
        {
            Actor.TryGetComponent(out healthUIMonoComponent);
        }
        
        public IActor Actor { get; set; }

        private void CurrentHealthOnChange(int obj)
        {
            healthUIMonoComponent.UpdateHealthUI();
        }
        public void LateStart()
        {
            IEntity player = Owner.World.GetSingleComponent<PlayerTagComponent>().Owner;
            player.TryGetHecsComponent(out HealthComponent healthComponent);
            healthComponent.currentHealth.OnChange += CurrentHealthOnChange;
            healthUIMonoComponent.InitHealthUI(healthComponent);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            IEntity player = Owner.World.GetSingleComponent<PlayerTagComponent>().Owner;
            player.TryGetHecsComponent(out HealthComponent healthComponent);
            healthComponent.currentHealth.OnChange -= CurrentHealthOnChange;
        }
    }
}