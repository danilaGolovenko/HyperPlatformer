using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlatformCatchPlayerSystem : BaseSystem, IReactCommand<Commands.Collision2dCommand>, IReactCommand<Commands.Collision2dExitCommand>
    {
        private ConcurrencyList<IEntity> players;

        public override void InitSystem()
        {
            players = Owner.World.Filter(HMasks.PlayerTagComponent);
        }


        public void CommandReact(Collision2dCommand command)
        {
            
            if (command.Collision.gameObject.TryGetComponent(out Actor actor) && actor.TryGetHecsComponent(out PlayerTagComponent playerTagComponent)) //gameObject вернул view, не entity
            {
                var player = command.Collision.gameObject;
                player.transform.SetParent(Owner.GetUnityTransformComponent().Transform);
            }
        }

        public void CommandReact(Collision2dExitCommand command)
        {
            if (command.Collision.gameObject.TryGetComponent(out Actor actor) && actor.TryGetHecsComponent(out PlayerTagComponent playerTagComponent))
            {
                var player = command.Collision.gameObject;
                player.transform.SetParent(null);
            }
        }
    }
}