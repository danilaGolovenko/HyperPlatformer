using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;
using Helpers;
using Unity.VisualScripting;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PortalSwitchSystem : BaseSystem, IReactCommand<Commands.Trigger2dEnterCommand>
    {
        [Required] private PortalScenesComponent portalScenesComponent;
        [Required] private SpawnPointComponent spawnPointComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (!command.Collider.TryGetActorFromCollision(out var actor) ||
                !actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent)) return;
            var switchSceneCommand = new SwitchSceneCommand
            {
                TargetSceneId = portalScenesComponent.TargetSceneIdentifier,
                CurrentSceneId = portalScenesComponent.CurrentSceneIdentifier,
                PortalSpawnPointPosition = spawnPointComponent.spawnPointTransform != null ? spawnPointComponent.spawnPointTransform.position : Vector3.zero
            };
            Owner.World.Command(switchSceneCommand);
        }

        public IActor Actor { get; set; }
    }
}