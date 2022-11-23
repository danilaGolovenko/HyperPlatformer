using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using System;
using System.Collections;
using System.Collections.Generic;
using Commands;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class EnemySpawnSystem : BaseSystem, ILateStart, IReactGlobalCommand<Commands.EnemySpawnCommand>
    {
        [Required] private EnemyContainerComponent enemyContainerComponent;
        private ConcurrencyList<IEntity> spawnPoints;
        private float waitTime = 5f;
        public override void InitSystem()
        {
            spawnPoints = Owner.World.Filter(HMasks.EnemySpawnPointTagComponent);
        }

        private async void SpawnEnemy()
        {
            IActor actor = await enemyContainerComponent.actorContainer.GetActor(true, null,
                Vector3.zero, default(Quaternion), spawnPoints.Data[0].GetUnityTransformComponent().Transform);
            actor.AddOrReplaceComponent(spawnPoints.Data[0].GetWayComponent());
            actor.Init();
        }
        
        public void LateStart()
        { 
            // if (spawnPoints.Data[0] != null)
                // SpawnEnemy();
        }

        public async void CommandGlobalReact(EnemySpawnCommand command)
        {
            WaitAndCallbackCommand callbackCommand = new WaitAndCallbackCommand();
            callbackCommand.CallBack = SpawnEnemy;
            callbackCommand.Timer = waitTime;
            Owner.World.Command(callbackCommand);
        }
    }
}