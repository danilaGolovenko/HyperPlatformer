using System;
using System.Collections;
using System.Threading.Tasks;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class RangeAttackSystem : BaseSystem, IReactCommand<Commands.AnimationEventCommand>, /*IFixedUpdatable,*/ IHaveActor
    {
        [Required] private RangeAttackComponent rangeAttackComponent;
        [Required] private WarheadHolderComponent warheadHolderComponent;
        [Required] private WaitComponent waitComponent;
        private RightSpawnProjectilePointMonoComponent rightSpawnProjectilePoint;
        private LeftSpawnProjectilePointMonoComponent leftSpawnProjectilePoint;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out rb);
            Actor.TryGetComponent(out rightSpawnProjectilePoint, true);
            Actor.TryGetComponent(out leftSpawnProjectilePoint, true);
            Actor.TryGetComponent(out spriteRenderer);
        }
        
        public IActor Actor { get; set; }
        public async void CommandReact(AnimationEventCommand command)
        {
            if (command.Id != AnimationEventIdentifierMap.Shooting) return;
            var isRightDirection = !spriteRenderer.flipX;
            var position = isRightDirection ? rightSpawnProjectilePoint.transform.position : leftSpawnProjectilePoint.transform.position;
            var actor = await warheadHolderComponent.WarheadHolderContainer.GetActor(true, null,
                position);
            var warheadComponent = actor.GetOrAddComponent<WarheadComponent>();
            warheadComponent.AttackPower = rangeAttackComponent.AttackPower;
            if (isRightDirection)
            {
                actor.GetOrAddComponent<SpeedCoeffComponent>().coefficient *= -1f;
            }
            actor.Init();
        }
    }
}