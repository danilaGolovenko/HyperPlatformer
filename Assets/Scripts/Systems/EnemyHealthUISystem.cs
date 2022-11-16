using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using Components;
using Components.MonoBehaviourComponents;
using UnityEngine;

namespace Systems
{
	[Serializable][Documentation(Doc.NONE, "")]
    public sealed class EnemyHealthUISystem : BaseSystem, IHaveActor, IReactCommand<Commands.InitEnemyHealthBarCommand>, ILateUpdatable
    {
        private EnemyHealthUIMonoComponent enemyHealthUIMonoComponent;
        [Required] private EntityHolderComponent enemyHolderComponent;
        private Transform enemyTransform;
        private Vector3 oldEnemyPosition;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out enemyHealthUIMonoComponent);
        }

        public void CommandReact(InitEnemyHealthBarCommand command)
        {
            if (enemyHolderComponent != null && enemyHolderComponent.entity != null)
            {
                enemyHolderComponent.entity.TryGetHecsComponent(out HealthComponent healthComponent);
                healthComponent.currentHealth.OnChange += CurrentHealthOnChange;
                enemyHealthUIMonoComponent.InitEnemyUI();
                var actor = (IActor)enemyHolderComponent.entity;
                if (actor.TryGetComponent(out enemyTransform))
                    oldEnemyPosition = enemyTransform.position;
            }
        }

        private void CurrentHealthOnChange(int obj)
        {
            enemyHealthUIMonoComponent.UpdateEnemyHealthUI();
        }

        public IActor Actor { get; set; }

        public void UpdateLateLocal()
        {
            // todo
            // https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html - про перевод координат
            
            // var s = enemyTransform.position - oldEnemyPosition;
            // transform.position += s;
            // oldEnemyPosition = enemyTransform.position;
        }
    }
}