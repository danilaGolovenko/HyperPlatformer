using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlatformCatchPlayerSystem : BaseSystem, IReactCommand<Commands.Collision2dCommand>, IReactCommand<Commands.Collision2dExitCommand> , IReactComponentGlobal<GroundedTagComponent>
        
        //ToDo
        //ГАЛОЧКА SIMULATED В rb
    {
        // private ConcurrencyList<IEntity> players;
        public override void InitSystem()
        {
            // players = Owner.World.Filter(HMasks.PlayerTagComponent);
        }


        public void CommandReact(Collision2dCommand command)
        {
            if (command.Collision.gameObject.TryGetComponent(out Actor actor) && actor.TryGetHecsComponent(out PlayerTagComponent playerTagComponent)) //gameObject вернул view, не entity
            {
                actor.TryGetComponent(out Rigidbody2D rb);
                rb.bodyType = RigidbodyType2D.Kinematic;
                // rb.simulated = false;
                var player = command.Collision.gameObject;
                player.transform.SetParent(Owner.GetUnityTransformComponent().Transform);
            }
        }

        public void CommandReact(Collision2dExitCommand command)
        {
            if (command.Collision.gameObject.TryGetComponent(out Actor actor) && actor.TryGetHecsComponent(out PlayerTagComponent playerTagComponent))
            {
                Debug.Log("Exit");
            //     actor.TryGetComponent(out Rigidbody2D rb);
            //     rb.simulated = true;
            //     var player = command.Collision.gameObject;
            //     player.transform.SetParent(null);
            }
        }

        public void ComponentReactGlobal(GroundedTagComponent component, bool isAdded)
        {
            if (!isAdded && component.Owner.TryGetHecsComponent(out PlayerTagComponent playerTag))
            {
                IActor actor = (IActor)component.Owner;
                actor.TryGetComponent(out Rigidbody2D rb);
                rb.bodyType = RigidbodyType2D.Dynamic;
                // rb.simulated = true;
                actor.TryGetComponent(out Transform transform);
                transform.SetParent(null);
            }
        }
    }
}