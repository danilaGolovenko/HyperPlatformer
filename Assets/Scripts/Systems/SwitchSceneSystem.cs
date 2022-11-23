using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class SwitchSceneSystem : BaseSystem, IReactGlobalCommand<Commands.SwitchSceneCommand>, IGlobalStart, IHaveActor
    {
        //todo загружать на старте 1ую сцену

        [Required] private SpawnPointComponent spawnPointComponent;
        private SwitchSceneMonoComponent switchSceneMonoComponent;
        
        public override void InitSystem()
        {
            if (!Actor.TryGetComponent(out switchSceneMonoComponent))
                Actor.RemoveHecsSystem(this);
        }
        public void CommandGlobalReact(SwitchSceneCommand command)
        {
            switchSceneMonoComponent.OnFadeComplete(command.targetSceneIndex);
        }

        public IActor Actor { get; set; }
        public void GlobalStart()
        {
            switchSceneMonoComponent.OnFadeComplete(1);
        }
    }
}