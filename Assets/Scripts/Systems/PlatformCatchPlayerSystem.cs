using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Helpers;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class PlatformCatchPlayerSystem : BaseSystem, IUpdatable, IHaveActor, IReactCommand<Trigger2dEnterCommand>, IReactCommand<Trigger2dExitCommand>
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

        public void UpdateLocal()
        {
            var s = platformRb.position - oldPosition;
            foreach (var rb in catchesListComponent.rbList)
            { 
                rb.position += s;
            }
            oldPosition = platformRb.position;
        }

        public IActor Actor { get; set; }
        public void CommandReact(Trigger2dEnterCommand command)
        {
            if (command.Collider.TryGetActorFromCollision(out var actor) && actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent)) 
            {
                actor.TryGetComponent(out Rigidbody2D rb);
                catchesListComponent.rbList.Add(rb);
            }
        }

        public void CommandReact(Trigger2dExitCommand command)
        {
            if (command.Collider.TryGetComponent(out Actor actor) && actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent))
            {
                actor.TryGetComponent(out Rigidbody2D rb);
                catchesListComponent.rbList.Remove(rb);
            }
        }
    }
}