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
        [Required] private HealthComponent healthComponent;
        [Required] private WaitComponent waitComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(DamageCommand command)
        {
            if (!command.authorEntity.Equals(Owner))
            {
                healthComponent.currentHealth.CurrentValue -= command.amount;
                Owner.AddHecsComponent(new StopMovingComponent());
                if (healthComponent.currentHealth.CurrentValue <= 0)
                {
                    DeathCommand deathCommand = new DeathCommand();
                    Owner.Command(deathCommand);
                }
                if (command.authorEntity.TryGetHecsComponent(out WaterTagComponent waterTagComponent))
                {
                    var spawnPoint = command.authorEntity.GetSpawnPointComponent().spawnPointTransform.position;
                    var waitTime = waitComponent.waitMonoComponent.waitTime;
                    waitComponent.waitMonoComponent.StartCoroutine(SendRespawnCommand(spawnPoint, waitTime));
                }
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