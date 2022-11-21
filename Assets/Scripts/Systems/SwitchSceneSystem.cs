using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class SwitchSceneSystem : BaseSystem, IReactGlobalCommand<Commands.SwitchSceneCommand>, IReactCommand<Commands.EventStateAnimationCommand>, IHaveActor
    {
        private SwitchSceneMonoComponent switchSceneMonoComponent;
        private int sceneToLoadIndex;
        public override void InitSystem()
        {
            if (!Actor.TryGetComponent(out switchSceneMonoComponent))
                Actor.RemoveHecsSystem(this);
        }

        public void CommandGlobalReact(SwitchSceneCommand command)
        {
            sceneToLoadIndex = command.targetSceneIndex;
            TriggerAnimationCommand commandFadeOut = new TriggerAnimationCommand();
            commandFadeOut.Index = AnimParametersMap.FadeOut;
            Owner.Command(commandFadeOut);
        }

        public void CommandReact(EventStateAnimationCommand command)
        {
            if (command.StateId == AnimatorStateIdentifierMap.Fade_Out)
            {
                switchSceneMonoComponent.OnFadeComplete(sceneToLoadIndex);
            }
        }

        public IActor Actor { get; set; }
    }
}