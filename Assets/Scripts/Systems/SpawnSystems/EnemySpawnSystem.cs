using System;
using System.Threading.Tasks;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class EnemySpawnSystem : BaseSystem, ILateStart, IHaveActor, IReactGlobalCommand<Commands.EnemySpawnCommand>
    {
        [Required] private EnemyContainerComponent enemyContainerComponent;
        private Transform transform;
        // todo вынести waitTime в компонент
        private int waitTime = 6000;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out transform);
        }

        // todo респавн после какого-то времени, если врага убили
        public async void LateStart()
        {
            var actor = await enemyContainerComponent.ActorContainer.GetActor(true, null,
                Vector3.zero, default(Quaternion), transform);
            actor.AddOrReplaceComponent(Owner.GetWayComponent());
            actor.Init();
        }

        public IActor Actor { get; set; }
        public async void CommandGlobalReact(EnemySpawnCommand command)
        {
            await Task.Delay(waitTime);
            if (!EntityManager.IsAlive) return;
            var actor = await enemyContainerComponent.ActorContainer.GetActor(true, null,
                Vector3.zero, default(Quaternion), transform);
            actor.AddOrReplaceComponent(Owner.GetWayComponent());
            actor.Init();
        }
    }
}