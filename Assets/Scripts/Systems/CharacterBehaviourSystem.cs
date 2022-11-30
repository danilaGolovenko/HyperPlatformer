using System;
using System.Threading.Tasks;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Helpers;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class CharacterBehaviourSystem : BaseSystem, IFixedUpdatable, IHaveActor
    {
        [Required] private CharacterStateComponent stateComponent;
        [Required] private RangeAttackComponent rangeAttackComponent;
        [Required] private SpeedCoeffComponent speedComponent;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private bool isTargetInRange = false;
        private bool isRightDirection = false;
        private int waitTime = 1000;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out spriteRenderer);
            Actor.TryGetComponent(out rb);
        }
        
        // public void CommandReact(Trigger2dEnterCommand command)
        // {
        //     if (!command.Collider.TryGetActorFromCollision(out var actor) ||
        //         !actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent)) return;
        //     isTargetInRange = true;
        // }
        //
        // public void CommandReact(Trigger2dExitCommand command)
        // {
        //     if (!command.Collider.TryGetActorFromCollision(out var actor) ||
        //         !actor.TryGetHecsComponent(out PlayerHolderComponent playerHolderComponent)) return;
        //     isTargetInRange = false;
        // }
        public async void FixedUpdateLocal()
        {
            var hit = Physics2D.CircleCast(rb.position, rangeAttackComponent.AttackRangeRadius, Vector2.zero, 0f,
                rangeAttackComponent.EnemyLayerMask);
            if (hit.collider == null) 
                isTargetInRange = false;
            else
            {
                hit.collider.gameObject.TryGetComponent(out Actor target);
                isRightDirection = target.GetUnityTransformComponent().Transform.transform.position.x > Owner.GetUnityTransformComponent().Transform.transform.position.x;
                isTargetInRange = true;
            }
            switch (stateComponent.currentState)
            {
                case CharacterState.Default:
                    stateComponent.currentState = CharacterState.Patrol;
                    break;
                case CharacterState.Patrol:
                {
                    speedComponent.coefficient = speedComponent.defaultCoefficient;
                    if (isTargetInRange)
                    {
                        stateComponent.currentState = CharacterState.PrepareToAttack;
                    }

                    break;
                }
                case CharacterState.PrepareToAttack:
                {
                    speedComponent.coefficient = 0;
                    spriteRenderer.flipX = !isRightDirection;
                    stateComponent.currentState =
                        isTargetInRange ? CharacterState.Attack : CharacterState.Patrol;
                    break;
                }
                case CharacterState.Attack:
                {
                    var commandIsAttacking = new BoolAnimationCommand
                    {
                        Index = AnimParametersMap.isAttacking,
                        Value = true
                    };
                    Owner.Command(commandIsAttacking);
                    spriteRenderer.flipX = !isRightDirection;
                    await Task.Delay(waitTime);
                    if (stateComponent == null)
                    {
                        return;
                    }
                    stateComponent.currentState = CharacterState.MakeDecisionAfterAttack;
                    break;
                }
                case CharacterState.MakeDecisionAfterAttack:
                {
                    var commandIsNotAttacking = new BoolAnimationCommand
                    {
                        Index = AnimParametersMap.isAttacking,
                        Value = false
                    }; 
                    Owner.Command(commandIsNotAttacking);
                    await Task.Delay(waitTime);
                    if (stateComponent == null)
                    {
                        return;
                    }
                    stateComponent.currentState =
                        isTargetInRange ? CharacterState.PrepareToAttack : CharacterState.Patrol;
                    break;
                }
                default:
                    stateComponent.currentState = CharacterState.Default;
                    break;
            }
        }

        public IActor Actor { get; set; }
    }
}