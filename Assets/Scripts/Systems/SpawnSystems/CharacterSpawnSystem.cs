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
            // var player = Owner.World.GetSingleComponent<PlayerTagComponent>().Owner;
            // characterContainerHolderComponent.CharacterContainer.GetComponent<PlayerHolderComponent>().PlayerEntity =
            // player;
            spawnPoints = Owner.World.Filter(HMasks.PlayerSpawnPointTagComponent);
            IActor actor = await characterContainerHolderComponent.CharacterContainer.GetActor(true, null,
            spawnPoints.Data[0].GetUnityTransformComponent().Transform.position);
            actor.Init();
        }
    }
}