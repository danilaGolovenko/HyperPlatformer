using System;
using Commands;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Systems
{
    public class MovingSystem : BaseSystem, IReactCommand<Commands.InputCommand>, IHaveActor, IReactCommand<Commands.InputEndedCommand>, IFixedUpdatable, IReactComponentLocal<StopMovingComponent>
    {
       
        private Rigidbody2D rb;
        private Transform transform;

        [Required] private CurrentSpeedComponent currentSpeed;
        [Required] private SpeedCoeffComponent speedCoeff;
        private bool isStoped = false;

        
        public override void InitSystem()
        {
            Actor.TryGetComponent(out transform);
            Actor.TryGetComponent(out rb);
            speedCoeff.coefficient = speedCoeff.defaultCoefficient;
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

            if (isStoped)
            {
                Owner.TryGetHecsComponent(out StopMovingComponent stopMovingComponent);
                stopMovingComponent.currentWaitTime -= Time.fixedDeltaTime;
                if (stopMovingComponent.currentWaitTime <= 0)
                {
                    Owner.RemoveHecsComponent(stopMovingComponent);
                }
            }
        }

        public void ComponentReact(StopMovingComponent component, bool isAdded)
        {
            if (isAdded)
            {
                speedCoeff.coefficient = 0;
                isStoped = true;
                Owner.TryGetHecsComponent(out StopMovingComponent stopMovingComponent);
                stopMovingComponent.currentWaitTime = stopMovingComponent.waitTIme;
            }
            else
            {
                speedCoeff.coefficient = speedCoeff.defaultCoefficient;
                isStoped = false;
            }
        }
    }
}