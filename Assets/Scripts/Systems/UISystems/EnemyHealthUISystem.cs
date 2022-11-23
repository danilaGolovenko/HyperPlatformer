using System;
using Cinemachine;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using Components;
using Components.MonoBehaviourComponents;
using UnityEngine;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class EnemyHealthUISystem : BaseSystem, IHaveActor, IReactCommand<InitEnemyHealthBarCommand>, ILateUpdatable
    {
        private EnemyHealthUIMonoComponent enemyHealthUIMonoComponent;
        [Required] private EntityHolderComponent enemyHolderComponent;
        private Transform enemyTransform;
        private HealthBarHeightComponent healthBarHeightComponent;
        private RectTransform healthBarRectTransform;
        private Camera camera;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out enemyHealthUIMonoComponent);
            Actor.TryGetComponent(out healthBarRectTransform);
            var cameraHolder = (IActor)Owner.World.GetSingleComponent<MainCameraTagComponent>().Owner;
            cameraHolder.TryGetComponent(out camera);
        }

        public void CommandReact(InitEnemyHealthBarCommand command)
        {
            if (enemyHolderComponent != null && enemyHolderComponent.entity != null)
            {
                enemyHolderComponent.entity.TryGetHecsComponent(out healthBarHeightComponent);
                enemyHolderComponent.entity.TryGetHecsComponent(out HealthComponent healthComponent);
                healthComponent.currentHealth.OnChange += CurrentHealthOnChange;
                enemyHealthUIMonoComponent.InitEnemyUI(healthComponent);
                var actor = (IActor)enemyHolderComponent.entity;
                if (actor.TryGetComponent(out enemyTransform))
                {
                    var rectScreenPoint = camera.WorldToScreenPoint(enemyTransform.position);
                    rectScreenPoint.z = 0;
                    rectScreenPoint.y += healthBarHeightComponent.height;
                    healthBarRectTransform.position = rectScreenPoint;
                }
            }
        }

        private void CurrentHealthOnChange(int obj)
        {
            enemyHealthUIMonoComponent.UpdateEnemyHealthUI();
        }

        public IActor Actor { get; set; }

        public void UpdateLateLocal()
        {
            if (enemyTransform != null)
            {
                // healthBarRectTransform.position - screen size
                // enemyTransform.position - world size
                var screenPoint = camera.WorldToScreenPoint(enemyTransform.position);
                screenPoint.z = 0;
                screenPoint.y += healthBarHeightComponent.height;
                healthBarRectTransform.position = screenPoint;
            }
        }
    }
}