using System;
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
        private Transform transform;

        [Required] private CurrentSpeedComponent currentSpeed;
        [Required] private SpeedCoeffComponent speedCoeff;

        
        public override void InitSystem()
        {
            Actor.TryGetComponent(out transform);
            Actor.TryGetComponent(out rb);
        }

        public void CommandReact(InputCommand command)
        {
            if (command.Index == InputIdentifierMap.Move)
            {
                var input = command.Context.ReadValue<Vector2>();
                var newSpeed = new Vector2(input.x*speedCoeff.coefficient, currentSpeed.speed.y);
                currentSpeed.speed = newSpeed;
            }
        }

        public IActor Actor { set; get; }
        public void CommandReact(InputEndedCommand command)
        {
            if (command.Index == InputIdentifierMap.Move)
            {
                var newSpeed = new Vector2(0, currentSpeed.speed.y);
                currentSpeed.speed = newSpeed;
            }
        }
        
        public void FixedUpdateLocal()
        {
            rb.velocity = Vector2.zero;
            float positionX = transform.position.x + currentSpeed.speed.x * Time.fixedDeltaTime;
            float positionY = transform.position.y + currentSpeed.speed.y * Time.fixedDeltaTime;
            Owner.GetUnityTransformComponent().Transform.position = new Vector3(positionX, positionY, 0);
        }
    }
}