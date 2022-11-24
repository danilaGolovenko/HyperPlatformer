using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class EnemySpawnSystem : BaseSystem, ILateStart, IHaveActor
    {
        [Required] private EnemyContainerComponent enemyContainerComponent;
        private Transform transform;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out transform);
        }

        public async void LateStart()
        {
            IActor actor = await enemyContainerComponent.ActorContainer.GetActor(true, null,
                Vector3.zero, default(Quaternion), transform);
            actor.AddOrReplaceComponent(Owner.GetWayComponent());
            actor.Init();
        }

        public IActor Actor { get; set; }
    }
}