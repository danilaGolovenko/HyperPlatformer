using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Unity.VisualScripting;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PortalSwitchSystem : BaseSystem, IReactCommand<Commands.Trigger2dEnterCommand>
    {
        [Required] private TargetSceneComponent targetSceneComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.gameObject.TryGetComponent(out Actor actor) && actor.TryGetHecsComponent(out PlayerTagComponent playerTagComponent)) 
            {
                SwitchSceneCommand switchSceneCommand = new SwitchSceneCommand();
                switchSceneCommand.targetSceneIndex = targetSceneComponent.sceneIndex;
                Owner.World.Command(switchSceneCommand);
            }
        }
    }
}