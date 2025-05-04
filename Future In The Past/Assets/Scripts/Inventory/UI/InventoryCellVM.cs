using UnityEngine;
using UnityEngine.UI;

namespace SibGameJam.Inventory.UI
{
    public class InventoryCellVM : MonoBehaviour
    {
        [SerializeField] private Sprite emptySprite;
        [SerializeField] private Image icon;

        public InventorySlot Slot { get; set; }

        private void Start()
        {
            Slot.OnSlotStatusUpdate += OnSlotUpdated;
            UpdateView();
        }

        private void OnSlotUpdated(object sender, CustomEventArgs.InventoryEventArgs e)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            Debug.Log($"Updating view to {Slot.ItemInfo?.name}");
            icon.sprite = Slot.ItemInfo?.Icon ?? emptySprite;
        }
    }
}
