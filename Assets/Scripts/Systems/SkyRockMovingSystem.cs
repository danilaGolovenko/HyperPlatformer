using System;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Systems
{
    [Serializable][Documentation(Doc.NONE, "")]
    public sealed class SkyRockMovingSystem : BaseSystem, IHaveActor
    {
        [Required] private SpeedCoeffComponent speedCoeffComponent;
        private Vector2 dir = Vector2.down;
        private Rigidbody2D rb;
        
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
            rb.velocity = speedCoeffComponent.coefficient * dir;
        }
        public IActor Actor { get; set; }
    }
}