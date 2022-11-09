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