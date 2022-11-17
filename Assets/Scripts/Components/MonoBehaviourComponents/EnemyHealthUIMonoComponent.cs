using HECSFramework.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class EnemyHealthUIMonoComponent : MonoBehaviour, IHaveActor
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fill;

        private HealthComponent enemyHealthComponent;

        public void InitEnemyUI(HealthComponent healthComponent)
        {
            enemyHealthComponent = healthComponent;
            slider.maxValue = enemyHealthComponent.maxHealth;
            slider.value = enemyHealthComponent.maxHealth;
            fill.color = gradient.Evaluate(1f);
        }

        public void UpdateEnemyHealthUI()
        {
            slider.value = enemyHealthComponent.currentHealth.CurrentValue;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        public IActor Actor { get; set; }
    }
}
