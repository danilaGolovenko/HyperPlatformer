using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using System.Threading.Tasks;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class SpawnSkyRocksSystem : BaseSystem, IFixedUpdatable, IAfterEntityInit
    {
        [Required] public SkyRockHolderComponent skyRockHolderComponent;
        private float rockfallRangeRadius = 0;
        private float rockfallRangeRadiusCoeff = 2f;
        private float heightOfRockSpawn = 0;
        /*private const int waitTimeInMilliseconds = 500;*/
        private const float waitTime = 0.5f;
        private float currentWaitTime = 0f;

        public override void InitSystem()
        {
        }
        public void AfterEntityInit()
        {
            if (Owner.TryGetHecsComponent<BossComponent>(out BossComponent bossComponent))
            {
                rockfallRangeRadius = bossComponent.RockfallRangeRadius * rockfallRangeRadiusCoeff;
                heightOfRockSpawn = bossComponent.HeightOfRockSpawn;
            }
        }
        public async void FixedUpdateLocal()
        {
            if (currentWaitTime > 0)
            {
                currentWaitTime -= Time.fixedDeltaTime;
                return;
            }

            //todo самый первый спавн - много камней 
            var position = Owner.GetUnityTransformComponent().Transform.transform.position;
            position.x = UnityEngine.Random.Range(position.x - rockfallRangeRadius, position.x + rockfallRangeRadius);
            position.y += heightOfRockSpawn;
            var actor = await skyRockHolderComponent.SkyRockHolderContainer.GetActor(true, null,
                position);
            actor.Init();

            currentWaitTime = waitTime;
            /*await Task.Delay(waitTimeInMilliseconds);*/
        }
    }
}