using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class MovingPlatformSystem : BaseSystem, IAfterEntityInit, IFixedUpdatable, IHaveActor
    {
        [Required] private WayComponent wayComponent;
        [Required] private SpeedCoeffComponent speedCoeffComponent;
        private Rigidbody2D rb;
        private Vector2 currentPoint;
        private Vector2 goalPoint;
        private int goalPointNumber = 0;
        private bool isForward = true;
        private float t = 0f;
        private float tCoeff = 0.0125f;
        public override void InitSystem()
        {
        }

        public void AfterEntityInit()
        {
            if (Actor.TryGetHecsComponent(out wayComponent) && wayComponent.listOfPoints.Count > 0)
            {
                currentPoint = wayComponent.listOfPoints[0];
                Actor.TryGetComponent(out rb);
                rb.position = currentPoint;
                if (wayComponent.listOfPoints.Count == 1)
                    goalPointNumber = 0;
                else
                    goalPointNumber = 1;
                goalPoint = wayComponent.listOfPoints[goalPointNumber];
                speedCoeffComponent.coefficient = speedCoeffComponent.defaultCoefficient;
            }
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
            var newPosition = Vector2.Lerp(currentPoint, goalPoint, t);
            rb.position = newPosition;
        }

        public IActor Actor { get; set; }
    }
}