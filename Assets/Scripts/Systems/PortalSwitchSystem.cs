using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;
using Unity.VisualScripting;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PortalSwitchSystem : BaseSystem, IReactCommand<Commands.Trigger2dEnterCommand>, IHaveActor
    {
        [Required] private TargetSceneComponent targetSceneComponent;
        private Transform portalSpawnPoint;
        public override void InitSystem()
        {
            if (Actor.TryGetComponent(out SpawnPointMonoComponent spawnPointMonoComponent))
            {
                portalSpawnPoint = spawnPointMonoComponent.SpawnPointTransform;
            }
            else
            {
                portalSpawnPoint = null;
            }
        }

        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.gameObject.TryGetComponent(out Actor actor) && actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent)) 
            {
                if (portalSpawnPoint != null){
                    // todo придумать, как запоминать/перемещать spawnPoint после портала. в PlayerContainer'е?
                }
                SwitchSceneCommand switchSceneCommand = new SwitchSceneCommand();
                switchSceneCommand.TargetSceneId = targetSceneComponent.SceneIdentifier.Id;
                Owner.World.Command(switchSceneCommand);
            }
        }

        public IActor Actor { get; set; }
    }
}