using System;
using System.Collections;
using System.Collections.Generic;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class TakeDamageSystem : BaseSystem, IReactCommand<Commands.DamageCommand>
    {
        private IHealthComponent healthComponent;
        
        [Required] private WaitComponent waitComponent;
        public override void InitSystem()
        {
            healthComponent = Owner.GetHECSComponent<IHealthComponent>();
        }

        public void CommandReact(DamageCommand command)
        {
            healthComponent.currentHealth.CurrentValue -= command.amount;
            Owner.AddHecsComponent(new StopMovingComponent());
            if (healthComponent.currentHealth.CurrentValue <= 0 && !Owner.TryGetHecsComponent(out DeadTagComponent deadTagComponent))
            {
                Owner.AddHecsComponent(new DeadTagComponent());
                Owner.Command(new DeathCommand());
            }
            if (command.authorEntity.TryGetHecsComponent(out WaterTagComponent waterTagComponent))    
            {
                var spawnPoint = command.authorEntity.GetSpawnPointComponent().spawnPointTransform.position;
                var waitTime = waitComponent.waitMonoComponent.waitTime;
                waitComponent.waitMonoComponent.StartCoroutine(SendRespawnCommand(spawnPoint, waitTime));
            }
        }

        private IEnumerator SendRespawnCommand(Vector2 spawnPoint, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            RespawnCommand respawnCommand = new RespawnCommand();
            respawnCommand.spawnPoint = spawnPoint;
            Owner.Command(respawnCommand);
        }
    }
}