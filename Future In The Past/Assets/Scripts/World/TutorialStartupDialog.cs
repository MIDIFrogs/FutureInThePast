using SibGameJam.Inventory;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class TutorialStartupDialog : StartupDialog
    {
        [Header("Tutorial")]
        [SerializeField] private PlayerInventory pastKolobok;
        [SerializeField] private ItemInfo toAdd;

        protected override void Start()
        {
            pastKolobok.TryAddItem(toAdd);
        }
    }
}