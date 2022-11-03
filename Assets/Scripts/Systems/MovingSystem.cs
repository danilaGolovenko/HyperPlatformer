using Commands;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Systems
{
    public class MovingSystem : BaseSystem, IReactCommand<Commands.InputCommand>, IHaveActor
    {
        private Rigidbody2D rb;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);

        }

        public void CommandReact(InputCommand command)
        {
            if (command.Index == InputIdentifierMap.Move)
            {
                var value = command.Context.ReadValue<Vector2>();
                rb.AddForce(new UnityEngine.Vector2(value.x*10, 0), ForceMode2D.Force);
            }
        }

        public IActor Actor { set; get; }
    }
}