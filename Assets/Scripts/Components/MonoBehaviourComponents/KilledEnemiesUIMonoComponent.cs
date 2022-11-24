using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class KilledEnemiesUIMonoComponent : MonoBehaviour
    {
        [field: SerializeField] public Text pointsText { get; private set; }
        private KilledEnemiesComponent playerKilledEnemiesComponent;

        public void InitKilledEnemiesUI(KilledEnemiesComponent killedEnemiesComponent)
        {
            playerKilledEnemiesComponent = killedEnemiesComponent;
            pointsText.text = playerKilledEnemiesComponent.ToString();
        }

        public void UpdateCurrentAmount()
        {
            pointsText.text = playerKilledEnemiesComponent.ToString();
        }
    }
}