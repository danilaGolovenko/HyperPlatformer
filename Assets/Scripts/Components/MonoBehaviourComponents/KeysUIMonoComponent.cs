using System.Collections;
using System.Collections.Generic;
using Components;
using UnityEngine;
using UnityEngine.UI;

public class KeysUIMonoComponent : MonoBehaviour
{
    [field: SerializeField] public Text pointsText { get; private set; }
    private PlayerKeysAmountComponent playerKeysAmountComponent;

    public void InitKeyUI(PlayerKeysAmountComponent keysAmountComponent)
    {
        playerKeysAmountComponent = keysAmountComponent;
        pointsText.text = playerKeysAmountComponent.amount.CurrentValue.ToString();
    }

    public void UpdateCurrentAmount()
    {
        pointsText.text = playerKeysAmountComponent.amount.CurrentValue.ToString();
    }
}
