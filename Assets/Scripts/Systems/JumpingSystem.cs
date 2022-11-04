using Commands;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using Helpers;
using UnityEngine;

namespace Systems 
{
    public class JumpingSystem : BaseSystem, IReactCommand<Commands.InputStartedCommand>, IReactCommand<Commands.Collision2dCommand>, IReactCommand<Commands.Collision2dExitCommand>,IHaveActor
    {
        private Rigidbody2D rb;
        [Required] private JumpForceComponent jumpForceComponent;
        [Required] private JumpingComponent jumpingComponent;
        private bool isGrounded;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
        }
        public void CommandReact(InputStartedCommand command)
        {
            if (command.Index == InputIdentifierMap.Jump && isGrounded)
            {
                rb.AddForce(new UnityEngine.Vector2(0, jumpForceComponent.jumpForce), ForceMode2D.Force);
            }
        }

        public IActor Actor { get; set; }
        public void CommandReact(Collision2dCommand command)
        {
            if (1 << command.Collision.gameObject.layer == jumpingComponent.layerMask)
            {
                isGrounded = true;  
            }
            // string layerName = LayerMask.LayerToName(command.Collision.gameObject.layer);
            // if (LayerMask.GetMask(layerName) == jumpingComponent.layerMask)
            //      isGrounded = true;  
        }

        public void CommandReact(Collision2dExitCommand command)
        {
            if (1 << command.Collision.gameObject.layer == jumpingComponent.layerMask)
            {
                isGrounded = false;  
            }
            
        }
    }
}