using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class MovingEnemySystem : BaseSystem, IAfterEntityInit, IFixedUpdatable, IHaveActor, IReactComponentLocal<StopMovingComponent>
    {
        [Required] private CurrentSpeedComponent currentSpeed;
        [Required] private WayComponent wayComponent;
        [Required] private SpeedCoeffComponent speedCoeffComponent;
        private Vector2 dir;
        private Rigidbody2D rb;
        private Vector2 currentPoint;
        private Vector2 goalPoint;
        private int goalPointNumber = 0;
        private bool isForward = true;
        private float t = 0f;
        private float tCoeff = 0.0125f;
        private bool isStoped = false;
        
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
        }

        public void AfterEntityInit()
        {
            if (!Actor.TryGetHecsComponent(out wayComponent) || wayComponent.listOfPoints.Count <= 0) return;
            currentPoint = wayComponent.listOfPoints[0];
            Actor.TryGetComponent(out rb);
            rb.position = currentPoint;
            goalPointNumber = wayComponent.listOfPoints.Count == 1 ? 0 : 1;
            goalPoint = wayComponent.listOfPoints[goalPointNumber];
            dir = (goalPoint - currentPoint).normalized;
            speedCoeffComponent.coefficient = speedCoeffComponent.defaultCoefficient;
            currentSpeed.speed = speedCoeffComponent.coefficient * dir;
        }

        private void ChangeGoalPoint()
        {
            currentPoint = goalPoint;
            
            if (wayComponent.listOfPoints.Count < 2)
                return;
            
            if (goalPointNumber == 0)
                isForward = true;
            
            if (goalPointNumber == wayComponent.listOfPoints.Count - 1)
                isForward = false;

            if (isForward)
                goalPointNumber++;
            else goalPointNumber--;

            goalPoint = wayComponent.listOfPoints[goalPointNumber];
            dir = (goalPoint - currentPoint).normalized;
        }
        
        public void FixedUpdateLocal()
        {
            if (t < 1)
                t += tCoeff*speedCoeffComponent.coefficient;
            else
            {
                t = 0;
                ChangeGoalPoint();
            }
            if (isStoped)
            {
                Owner.TryGetHecsComponent(out StopMovingComponent stopMovingComponent);
                stopMovingComponent.currentWaitTime -= Time.fixedDeltaTime;
                if (stopMovingComponent.currentWaitTime <= 0)
                {
                    Owner.RemoveHecsComponent(stopMovingComponent);
                }
                return;
            }
            else
            {
                var newPosition = Vector2.Lerp(currentPoint, goalPoint, t);
                rb.position = newPosition;
            }
            currentSpeed.speed = speedCoeffComponent.coefficient * dir;
            
        }

        public IActor Actor { get; set; }
        
        public void ComponentReact(StopMovingComponent component, bool isAdded)
        {
            if (isAdded)
            {
                speedCoeffComponent.coefficient = 0;
                isStoped = true;
                component.currentWaitTime = component.waitTIme;
            }
            else
            {
                speedCoeffComponent.coefficient = speedCoeffComponent.defaultCoefficient;
                isStoped = false;
            }
        }
    }
}