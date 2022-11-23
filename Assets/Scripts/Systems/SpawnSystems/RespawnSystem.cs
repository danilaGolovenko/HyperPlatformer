using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class RespawnSystem : BaseSystem, IReactCommand<Commands.RespawnCommand>
    {
        public override void InitSystem()
        {
        }

        public void CommandReact(RespawnCommand command)
        {
            Owner.GetUnityTransformComponent().Transform.position = command.spawnPoint;
        }
    }
}