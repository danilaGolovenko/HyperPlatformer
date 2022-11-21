using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class KilledEnemiesUIMonoComponent : MonoBehaviour
    {
        [field: SerializeField] public Text pointsText { get; private set; }
        private WinPointsComponent playerWinPointsComponent;

        public void InitKilledEnemiesUI(WinPointsComponent winPointsComponent)
        {
            playerWinPointsComponent = winPointsComponent;
            pointsText.text = playerWinPointsComponent.ToString();
        }

        public void UpdateCurrentAmount()
        {
            pointsText.text = playerWinPointsComponent.ToString();
        }
    }
}