using System.Collections.Generic;
using HECSFramework.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class PlayerInventoryUIMonoComponent : MonoBehaviour
    {
        [field: SerializeField] public GameObject SlotPrefab { get; private set; }
        private RectTransform playerInventoryUITransform;
        private readonly List<GameObject> oldSlots = new List<GameObject>();

        public void InitInventoryUI(List<Item> inventory)
        {
            playerInventoryUITransform = GetComponent<RectTransform>();
            foreach (var item in inventory)
            {
                var slot = Instantiate (SlotPrefab, playerInventoryUITransform, true);
                var image = slot.GetComponentInChildren<Image>();
                var text = slot.GetComponentInChildren<Text>();
                image.sprite = item.itemContainer.GetComponent<IconHolderComponent>().iconSprite;
                text.text = item.amount.ToString();
                oldSlots.Add(slot);
            }
        }
        
        public void UpdateInventoryUI(List<Item> inventory)
        {
            foreach (var slot in oldSlots)
            {
                Destroy(slot);
            }
            oldSlots.Clear();
            
            foreach (var item in inventory)
            {
                var slot = Instantiate (SlotPrefab, playerInventoryUITransform, true);
                var image = slot.GetComponentInChildren<Image>();
                var text = slot.GetComponentInChildren<Text>();
                image.sprite = item.itemContainer.GetComponent<IconHolderComponent>().iconSprite;
                text.text = item.amount.ToString();
                oldSlots.Add(slot);
            }
        }
        
    }
}
