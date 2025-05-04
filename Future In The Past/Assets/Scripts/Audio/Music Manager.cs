using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    using UnityEngine;

    public class MusicManager : MonoBehaviour
    {
        public enum LocationType
        {
            Main,
            Footer,
            Forest
        }

        [System.Serializable]
        public class Transition
        {
            public AudioClip sound; 
            public LocationType currentLocation; 
            public LocationType nextLocation; 
        }

        [System.Serializable]
        public class Track
        {
            public AudioClip music; 
            public Transition transition1; 
            public Transition transition2;
        }

        [System.Serializable]
        public class Location
        {
            public GameObject bottomLeftObj;
            [HideInInspector] public Vector2 bottomLeft;
            public GameObject topRightObj;
            [HideInInspector] public Vector2 topRight;
            public Track track; 
            public LocationType locationType; 
        }

        public Location[] locations; 
        private Track currentTrack; 
        private Track newTrack; 
        private AudioSource audioSource; 

        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            InitializeTracks();
            currentTrack = locations[0].track; 
            PlayMusic(currentTrack);
        }

        void Update()
        {
            CheckPlayerLocation();
        }

        void InitializeTracks()
        {
            // Здесь можно инициализировать треки и локации, если это необходимо
        }

        void CheckPlayerLocation()
        {
            Vector2 playerPosition = transform.position; 

            foreach (var location in locations)
            {
                location.bottomLeft = location.bottomLeftObj.transform.position;
                location.topRight = location.topRightObj.transform.position;
                if (playerPosition.x >= location.bottomLeft.x && playerPosition.x <= location.topRight.x &&
                    playerPosition.y >= location.bottomLeft.y && playerPosition.y <= location.topRight.y)
                {
                    newTrack = location.track; 

                    if (newTrack != currentTrack)
                    {
                        PlayTransition(currentTrack, newTrack, location);
                        currentTrack = newTrack;
                    }
                    break;
                }
            }
        }

        void PlayMusic(Track track)
        {
            audioSource.clip = track.music;
            audioSource.Play();
        }

        void PlayTransition(Track oldTrack, Track newTrack, Location location)
        {
            Transition transition = GetTransition(oldTrack, newTrack, location);
            if (transition != null)
            {
                audioSource.Stop();
                audioSource.clip = transition.sound;
                audioSource.Play();
                Invoke("PlayNewMusic", audioSource.clip.length);
            }
        }

        Transition GetTransition(Track oldTrack, Track newTrack, Location location)
        {
            if (oldTrack.transition1.nextLocation == location.locationType)
                return oldTrack.transition1;
            if (oldTrack.transition2.nextLocation == location.locationType)
                return oldTrack.transition2;

            return null;
        }

        void PlayNewMusic()
        {
            PlayMusic(currentTrack);
        }
    }

}
