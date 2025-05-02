using TMPro;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    public class DialogHistoryEntry : MonoBehaviour
    {
        [Header("Dialog properties")]
        [SerializeField] private TMP_Text AuthorTag;
        [SerializeField] private TMP_Text ReplicText;

        public void PlaceReplic(Replic replic)
        {
            AuthorTag.text = replic.Author.Name;
            AuthorTag.color = replic.Author.SignColor;

            ReplicText.text = replic.Message;
            ReplicText.fontStyle = replic.FontStyle;
        }
    }
}
