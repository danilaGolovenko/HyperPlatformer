using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class DeathHandlerSystem : BaseSystem, IReactGlobalCommand<Commands.DeathCommand>
    {
        public override void InitSystem()
        {
        }

        public void CommandGlobalReact(DeathCommand command)
        {
            Debug.Log("=== " + command.entity + " --- RIP ===");
            BoolAnimationCommand commandIsDead = new BoolAnimationCommand();
            commandIsDead.Index = AnimParametersMap.isDead;
            commandIsDead.Value = true;
            Owner.Command(commandIsDead);
        }
    }
}