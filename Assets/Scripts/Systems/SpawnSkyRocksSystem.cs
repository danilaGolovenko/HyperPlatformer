using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class SpawnSkyRocksSystem : BaseSystem, IFixedUpdatable
    {
        [Required] public SkyRockHolderComponent skyRockHolderComponent;
        private const float waitTime = 1f;
        private float currentWaitTime = 0f;
        public override void InitSystem()
        {
        }
        public async void FixedUpdateLocal()
        {
            //todo по камню через waitTime (сделать через cooldown) на position=random(в диапазоне) 
            if (currentWaitTime > 0)
            {
                currentWaitTime -= Time.fixedDeltaTime;
                return;
            }
            var position = Owner.GetUnityTransformComponent().Transform.transform.position;
            var actor = await skyRockHolderComponent.SkyRockHolderContainer.GetActor(true, null,
                position);
            actor.Init();
            currentWaitTime = waitTime;
        }
    }
}