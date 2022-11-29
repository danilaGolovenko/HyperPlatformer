using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class WarheadMovingSystem : BaseSystem, IHaveActor
    {
        [Required] private SpeedCoeffComponent speedCoeffComponent;
        //todo dir
        private Vector2 dir = Vector2.left;
        private Rigidbody2D rb;
        
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
            rb.velocity = speedCoeffComponent.coefficient * dir;
        }
        public IActor Actor { get; set; }
    }
}