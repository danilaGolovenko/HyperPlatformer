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
    public sealed class TagingGroundedSystem : BaseSystem, IUpdatable, IHaveActor
    {
        
        [Required] private SubjectToGravityTagComponent subjectToGravityTagComponent;
        private Transform transform;
        private float rayLength;
        private float distance;
        
        public override void InitSystem()
        {
            Actor.TryGetComponent(out transform);
            rayLength = 100f;
            distance = 0f;
        }
        
        public IActor Actor { get; set; }
        public void UpdateLocal()
        {
            // RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 0.02f, Vector2.do1fwn, rayLength, subjectToGravityTagComponent.groudLayerMask);
            // var isCollidedWithGround = Physics2D.BoxCast(transform.position, new Vector3(1f, 1f, 1f), Vector2.down, out RaycastHit hitInfo,
            //     transform.rotation, subjectToGravityTagComponent.groudLayerMask);
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.1f, 0.3f), 0f, Vector2.down, distance,
                subjectToGravityTagComponent.groudLayerMask);
            // if (hit.collider != null && Math.Abs(transform.position.y - hit.point.y) <= distance)
            if (hit.collider != null && Mathf.Abs(transform.position.y - hit.point.y) <= 0.1)
            {
                // IActor actor = (IActor)Owner;
                // actor.TryGetComponent(out Transform transform);
                // var position = transform.position;
                // position = new Vector3(position.x, position.y - 0.1f, position.z);
                // Debug.Log(position);
                // transform.position = position;
                if (!Owner.ContainsMask<GroundedTagComponent>())
                {
                    Owner.AddHecsComponent(new GroundedTagComponent {});
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
    }
}