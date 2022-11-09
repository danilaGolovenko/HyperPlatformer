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
            distance = 0.1f;
        }
        
        public IActor Actor { get; set; }
        public void UpdateLocal()
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.1f, 0.3f), 0f, Vector2.down, 0f,
                subjectToGravityTagComponent.groudLayerMask);
            if (hit.collider != null && Mathf.Abs(transform.position.y - hit.point.y) <= distance)
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