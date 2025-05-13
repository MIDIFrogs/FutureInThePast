using System.Collections.Generic;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private float transitionDuration;

        private SortedSet<AmbientMusicArea> latestAreas = new(new AreaComparer());

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent<AmbientMusicArea>(out var area))
                {
                    area.PlayerEntered += OnPlayerEntered;
                    area.PlayerLeft += OnPlayerLeft;
                }
            }
        }

        private void OnPlayerLeft(object sender, System.EventArgs e)
        {
            if (sender is AmbientMusicArea area && latestAreas.Contains(area))
            {
                latestAreas.Remove(area);
            }
        }

        private void OnPlayerEntered(object sender, System.EventArgs e)
        {
            if (sender is AmbientMusicArea area)
            {
                latestAreas.Add(area);
            }
        }

        private class AreaComparer : IComparer<AmbientMusicArea>
        {
            public int Compare(AmbientMusicArea x, AmbientMusicArea y) => x.GetHashCode() - y.GetHashCode();
        }
    }
}
