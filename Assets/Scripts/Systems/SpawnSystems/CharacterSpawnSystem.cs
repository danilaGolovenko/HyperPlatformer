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
            var startSpawnPointPosition = Vector3.zero;

            var afterPortalSpawnPointsComponent = Owner.World.GetSingleComponent<PlayerTagComponent>().Owner.GetAfterPortalSpawnPointsComponent();
           
            if (afterPortalSpawnPointsComponent?.SpawnPoints != null && command.SceneIdentifier != null 
                                                                     && afterPortalSpawnPointsComponent.SpawnPoints.ContainsKey(command.SceneIdentifier))
            {
                startSpawnPointPosition = afterPortalSpawnPointsComponent.SpawnPoints[command.SceneIdentifier];
            }
            else
            {
                spawnPoints = Owner.World.Filter(HMasks.PlayerSpawnPointTagComponent);
                foreach (var point in spawnPoints)
                {
                    if (point.GetPlayerSpawnPointIdentifierComponent().PlayerSpawnPointIdentifier.Id !=
                        PlayerSpawnPointIdentifierMap.StartSpawnPoint_identifier) continue;
                    startSpawnPointPosition = point.GetUnityTransformComponent().Transform.position;
                    break;
                }
            }
            var actor = await characterContainerHolderComponent.CharacterContainer.GetActor(true, null,
                startSpawnPointPosition);
            actor.Init();
        }
    }
}