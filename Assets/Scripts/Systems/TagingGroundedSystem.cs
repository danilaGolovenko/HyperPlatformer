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
    public sealed class TagingGroundedSystem : BaseSystem, ILateUpdatable, IHaveActor
    {
        
        [Required] private SubjectToGravityTagComponent subjectToGravityTagComponent;
        private Transform transform;
        private float distance;
        
        public override void InitSystem()
        {
            Actor.TryGetComponent(out transform);
            distance = 0.1f;
            Physics2D.queriesHitTriggers = false;
        }
        
        public IActor Actor { get; set; }
        public void UpdateLateLocal()
        {
            var hit = Physics2D.BoxCast(transform.position, new Vector2(0.1f, 0.3f), 0f, Vector2.down, 0f,
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