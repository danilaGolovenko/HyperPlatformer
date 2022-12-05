using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class BossTriggerRangeUpdateSystem : BaseSystem, ILateUpdatable, IHaveActor
    {
        private BoxCollider2D boxCollider2D;
        private SpriteRenderer spriteRenderer;
        private bool cachedFlipX = false;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out spriteRenderer);
            Actor.TryGetComponent(out boxCollider2D);
            cachedFlipX = spriteRenderer.flipX;
        }

        public void UpdateLateLocal()
        {
            if (spriteRenderer.flipX == cachedFlipX) return;
            cachedFlipX = spriteRenderer.flipX;
            var offset = boxCollider2D.offset;
            offset = new Vector2(offset.x * -1f, offset.y);
            boxCollider2D.offset = offset;
        }

        public IActor Actor { get; set; }
    }
}