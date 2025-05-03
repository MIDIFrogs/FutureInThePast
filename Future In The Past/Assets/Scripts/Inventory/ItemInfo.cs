using UnityEngine;

namespace SibGameJam.Inventory
{
    [CreateAssetMenu(fileName = "New Item Info", menuName = "Items/Item definition")]
    public class ItemInfo : ScriptableObject
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;

        public string Title => title;
        public string Description => description;
        public Sprite Icon => icon;
    }
}