using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Commands;
using JetBrains.Annotations;
using Unity.VisualScripting;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class ChargeMovingSystem : BaseSystem, IHaveActor, IAfterEntityInit, IFixedUpdatable, 
        IReactCommand<Commands.StartChargeCommand>, IReactCommand<Commands.EventStateAnimationCommand>, IReactCommand<Trigger2dEnterCommand>
    {
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private Transform transform;

        private float goalPointX = 0;
        private float speedCoeff = 0;
        private int dir = 1;
        private bool chargeMoving = false;

        public IActor Actor { get; set; }

        public void AfterEntityInit()
        {
            Actor.TryGetComponent(out rb);
            Actor.TryGetComponent(out spriteRenderer);
            Actor.TryGetComponent(out transform);
        }

        public override void InitSystem()
        {
        }
        public void CommandReact(StartChargeCommand command)
        {
            goalPointX = command.GoalPointX;
            speedCoeff = command.SpeedCoeff;
        }

        public void FixedUpdateLocal()
        {
            if (!chargeMoving) { return; }

            float ownerX = transform.position.x;
            float ownerY = transform.position.y;

            if (transform.position.x != goalPointX)
            {
                dir = goalPointX > ownerX ? 1 : -1;
                ownerX += speedCoeff * dir * Time.fixedDeltaTime;
                transform.position = new Vector2(ownerX, ownerY);
            }
            else
            {
                chargeMoving = false;
            }
        }

        public void CommandReact(EventStateAnimationCommand command)
        {
            if (command.StateId == AnimatorStateIdentifierMap.Boss_Charge)
            {
                if (command.AnimationId == AnimationEventIdentifierMap.StartEnemyAttack)
                    chargeMoving = true;
                if (command.AnimationId == AnimationEventIdentifierMap.Shooting)
                    chargeMoving = false;
            }
        }

        public void CommandReact(Trigger2dEnterCommand command)
        {
            speedCoeff = 0;
            chargeMoving = false;
        }
    }
}