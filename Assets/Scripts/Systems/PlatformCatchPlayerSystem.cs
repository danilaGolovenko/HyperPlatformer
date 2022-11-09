using System;
using System.Collections.Generic;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Gamekit2D;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlatformCatchPlayerSystem : BaseSystem, IFixedUpdatable, IReactCommand<Collision2dCommand>, IReactCommand<Collision2dExitCommand>, IHaveActor
    {
        [Required] private CatchesListComponent catchesListComponent;
        private Rigidbody2D platformRb;
        private Vector2 oldPosition;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out platformRb);
            Actor.TryGetComponent(out platformRb);
            oldPosition = platformRb.position;
        }


         public void CommandReact(Collision2dCommand command)
         {
             Debug.Log("Collision");
             if (command.Collision.gameObject.TryGetComponent(out Actor actor) && actor.TryGetHecsComponent(out PlayerTagComponent playerTagComponent)) 
             {
                 actor.TryGetComponent(out Rigidbody2D rb);
                 catchesListComponent.rbList.Add(rb);
                 // rb.bodyType = RigidbodyType2D.Kinematic;
                 // rb.simulated = false;
                 // var player = command.Collision.gameObject;
                 // player.transform.SetParent(Owner.GetUnityTransformComponent().Transform);
             }
         }

         public void CommandReact(Collision2dExitCommand command)
         {
             if (command.Collision.gameObject.TryGetComponent(out Actor actor) && actor.TryGetHecsComponent(out PlayerTagComponent playerTagComponent))
             {
                 actor.TryGetComponent(out Rigidbody2D rb);
                 catchesListComponent.rbList.Remove(rb);
             }
         }

        public void FixedUpdateLocal()
        {
            var s = platformRb.position - oldPosition;
            foreach (var rb in catchesListComponent.rbList)
            {
                rb.MovePosition(rb.position + s);
            }
            oldPosition = platformRb.position;
        }

        public IActor Actor { get; set; }
    }
}