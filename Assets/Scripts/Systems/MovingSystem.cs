using Commands;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Systems
{
    public class MovingSystem : BaseSystem, IReactCommand<Commands.InputCommand>, IHaveActor, IReactCommand<Commands.InputEndedCommand>, IFixedUpdatable
    {
        private Rigidbody2D rb;

        [Required] private SpeedComponent speedComponent;
        
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
        }

        public void CommandReact(InputCommand command)
        {
            if (command.Index == InputIdentifierMap.Move)
            {
                var value = command.Context.ReadValue<Vector2>();
                rb.velocity = new Vector2(value.x * speedComponent.Speed, rb.velocity.y);

                // rb.AddForce(new UnityEngine.Vector2(value.x*10, 0), ForceMode2D.Force);
            }
        }

        public IActor Actor { set; get; }
        public void CommandReact(InputEndedCommand command)
        {
            if (command.Index == InputIdentifierMap.Move)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        public void FixedUpdateLocal()
        {
            rb.AddForce(new Vector2(0, -9.8f*60*Time.fixedDeltaTime), ForceMode2D.Force);
        }
    }
}