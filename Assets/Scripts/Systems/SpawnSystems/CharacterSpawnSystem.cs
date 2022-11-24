using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class CharacterSpawnSystem : BaseSystem, IReactGlobalCommand<LoadedSceneCommand>
    {
        [Required] private CharacterContainerHolderComponent characterContainerHolderComponent;
        private ConcurrencyList<IEntity> spawnPoints;
        
        public override void InitSystem()
        {
        }
        
        public async void CommandGlobalReact(LoadedSceneCommand command)
        {
            spawnPoints = Owner.World.Filter(HMasks.PlayerSpawnPointTagComponent);
            Vector3 startSpawnPointPosition = Vector3.zero;
            foreach (var point in spawnPoints)
            {
                if (point.GetPlayerSpawnPointIdentifierComponent().PlayerSpawnPointIdentifier.Id ==
                    PlayerSpawnPointIdentifierMap.StartSpawnPoint_identifier)
                {
                    startSpawnPointPosition = point.GetUnityTransformComponent().Transform.position;
                    break;
                }
            }
            IActor actor = await characterContainerHolderComponent.CharacterContainer.GetActor(true, null,
                startSpawnPointPosition);
            actor.Init();
        }
    }
}