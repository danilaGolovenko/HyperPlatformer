using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlayerSpawnSystem : BaseSystem, ILateStart
    {
        [Required] private PlayerContainerComponent playerContainerComponent;
        private ConcurrencyList<IEntity> spawnPoints;
        public override void InitSystem()
        {
            spawnPoints = Owner.World.Filter(HMasks.PlayerSpawnPointTagComponent);
        }

        public async void LateStart()
        {
            IActor actor = await playerContainerComponent.actorContainer.GetActor(true, null,
                spawnPoints.Data[0].GetUnityTransformComponent().Transform.position);
            
            actor.Init();
        }
    }
}