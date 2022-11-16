using HECSFramework.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class EnemyHealthUIMonoComponent : MonoBehaviour, IHaveActor
    {
        private Slider slider;
        private Gradient gradient;
        private Image fill;

        private HealthComponent enemyHealthComponent;

        public void InitEnemyUI()
        {
            slider = GetComponent<Slider>();
            gradient = GetComponent<Gradient>();
            fill = GetComponent<Image>();
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
