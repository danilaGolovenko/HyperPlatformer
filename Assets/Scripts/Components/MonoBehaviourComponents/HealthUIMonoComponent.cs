using UnityEngine;

namespace Components.MonoBehaviourComponents
{
    public class HealthUIMonoComponent : MonoBehaviour
    {
        [field: SerializeField] public GameObject healthIconPrefab { get; private set; }
        
        private Animator[] healthIconAnimators;

        private readonly int isActive = Animator.StringToHash ("isActive");
        private readonly int inactiveState = Animator.StringToHash ("HealthIconInactive");

        private RectTransform healthUIRectTransform;
        private HealthComponent playerHealthComponent;


        public void InitHealthUI(HealthComponent healthComponent)
        {
            playerHealthComponent = healthComponent;
            healthUIRectTransform = GetComponent<RectTransform>();
            
            int maxHealth = playerHealthComponent.maxHealth;
            healthIconAnimators = new Animator[maxHealth];
                
            for (int i = 0; i < maxHealth; i++)
            {
                GameObject healthIcon = Instantiate (healthIconPrefab, healthUIRectTransform, true);
                healthIconAnimators[i] = healthIcon.GetComponent<Animator> ();

                if (playerHealthComponent.currentHealth.CurrentValue < i + 1)
                {
                    healthIconAnimators[i].Play (inactiveState);
                    healthIconAnimators[i].SetBool (isActive, false);
                }
            }
        }
        
        public void UpdateHealthUI()
        {
            if(healthIconAnimators == null)
                return;
            
            for (int i = 0; i < healthIconAnimators.Length; i++)
            {
                healthIconAnimators[i].SetBool(isActive, playerHealthComponent.currentHealth.CurrentValue >= i + 1);
            }
        }
    }
}