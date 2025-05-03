using UnityEngine;

namespace MIDIFrogs.FutureInThePast.UI.HUD
{
    public class HudRoot : MonoBehaviour
    {
        [SerializeField] private GameObject pastHud;
        [SerializeField] private GameObject futureHud;
        [SerializeField] private GameObject pastPlayer;
        [SerializeField] private GameObject futurePlayer;

        private void Update()
        {
            pastHud.SetActive(pastPlayer.activeInHierarchy);
            futureHud.SetActive(futurePlayer.activeInHierarchy);
        }
    }
}