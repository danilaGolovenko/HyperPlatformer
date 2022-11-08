using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Unity.VisualScripting;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class TagingGroundedSystem : BaseSystem, IFixedUpdatable, IHaveActor
    {
        
        [Required] private SubjectToGravityTagComponent subjectToGravityTagComponent;
        private Transform transform;
        private float rayLength;
        private float distance;
        
        public override void InitSystem()
        {
            Actor.TryGetComponent(out transform);
            rayLength = 100f;
            Actor.TryGetComponent(out CapsuleCollider2D collider2D);
            distance = 0.2f;
        }
        
        public IActor Actor { get; set; }
        public void FixedUpdateLocal()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, subjectToGravityTagComponent.groudLayerMask);
            if (hit.collider != null && (transform.position.y - hit.point.y) <= distance)
            {
                if (!Owner.ContainsMask<GroundedTagComponent>())
                {
                    Owner.AddHecsComponent(new GroundedTagComponent {});
                    Debug.Log("Ku");
                }
            }
            else
            {
                if (Owner.ContainsMask<GroundedTagComponent>())
                {
                    Owner.RemoveHecsComponent<GroundedTagComponent>();
                }
            }
        }

        // public void CommandReact(Collision2dCommand command)
        // {
        //     startPoint = transform.position;
        //     RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.down, rayLength, subjectToGravityTagComponent.groudLayerMask);
        //     // Color rayColor = Color.red;
        //     // Debug.DrawRay(Owner.GetUnityTransformComponent().Transform.position, Vector2.down * distance, rayColor);
        //     if (hit.collider != null && (transform.position.y - hit.point.y) <= distance)
        //     {
        //         if (!Owner.ContainsMask<GroundedTagComponent>())
        //         {
        //             Owner.AddHecsComponent(new GroundedTagComponent {});
        //             Debug.Log("Grounded; " + hit.distance + "; " + distance);
        //         }
        //     }
            // if (1 << command.Collision.gameObject.layer == subjectToGravityTagComponent.groudLayerMask)
            // {
            //     if (!Owner.ContainsMask<GroundedTagComponent>())
            //     {
            //         Owner.AddHecsComponent(new GroundedTagComponent {});
            //     }
            // }
            // string layerName = LayerMask.LayerToName(command.Collision.gameObject.layer);
            // if (LayerMask.GetMask(layerName) == jumpingComponent.layerMask)
            //      isGrounded = true;  
        // }
        
        // public void CommandReact(Collision2dExitCommand command)
        // {
        //     RaycastHit2D hit = Physics2D.Raycast(startPoint, Vector2.down, distance, subjectToGravityTagComponent.groudLayerMask);
        //     if (hit.collider == null)
        //     {
        //     if (1 << command.Collision.gameObject.layer == subjectToGravityTagComponent.groudLayerMask)
        //     {
        //         if (Owner.ContainsMask<GroundedTagComponent>())
        //         {
        //             Owner.RemoveHecsComponent<GroundedTagComponent>();
        //         }
        //     }

            // if (1 << command.Collision.gameObject.layer == subjectToGravityTagComponent.groudLayerMask)
            // {
            //     Owner.RemoveHecsComponent<GroundedTagComponent>();
            // }
            
        // }
    }
}