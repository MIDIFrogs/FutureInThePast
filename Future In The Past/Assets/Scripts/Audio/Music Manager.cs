using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class MusicManager : MonoBehaviour
    {
        public enum LocationType
        {
            Main,
            Footer,
            Forest
        }

        public GameObject player;

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
            public AudioClip music; // Обычная версия
            public AudioClip alternativeMusic; // Альтернативная версия
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
            public bool visited; // Поле для отслеживания посещенности
        }

        public Location[] locations;
        private Track currentTrack;
        private Track newTrack;
        private AudioSource audioSource;
        private bool isTransitioning = false;

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
            CheckMusicEnd();
        }

        void InitializeTracks()
        {
            // Здесь можно инициализировать треки и локации, если это необходимо
        }

        void CheckPlayerLocation()
        {
            Vector2 playerPosition = player.transform.position;

            foreach (var location in locations)
            {
                location.bottomLeft = location.bottomLeftObj.transform.position;
                location.topRight = location.topRightObj.transform.position;
                if (playerPosition.x >= location.bottomLeft.x && playerPosition.x <= location.topRight.x &&
                    playerPosition.y >= location.bottomLeft.y && playerPosition.y <= location.topRight.y)
                {
                    newTrack = location.track;

                    if (newTrack != currentTrack && !isTransitioning && !audioSource.isPlaying)
                    {
                        PlayTransition(currentTrack, newTrack, location);
                        currentTrack = newTrack;
                    }

                    // Проверяем, посещалась ли локация
                    if (!location.visited)
                    {
                        location.visited = true; // Отмечаем локацию как посещенную
                        PlayMusic(newTrack); // Играем обычную версию
                    }
                    else
                    {
                        PlayAlternativeMusic(newTrack); // Играем альтернативную версию
                    }
                    break;
                }
            }
        }

        void PlayMusic(Track track)
        {
            audioSource.clip = track.music;
            audioSource.Play();
            Debug.Log($"Playing music: {track.music.name}");
        }

        void PlayAlternativeMusic(Track track)
        {
            audioSource.clip = track.alternativeMusic;
            audioSource.Play();
            Debug.Log($"Playing alternative music: {track.alternativeMusic.name}");
        }

        void PlayTransition(Track oldTrack, Track newTrack, Location location)
        {
            Transition transition = GetTransition(oldTrack, newTrack, location);
            if (transition != null)
            {
                audioSource.Stop();
                audioSource.clip = transition.sound;
                audioSource.Play();
                isTransitioning = true;
                Debug.Log($"Playing transition from {oldTrack.music.name} to {newTrack.music.name}");
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
            isTransitioning = false;
            Debug.Log($"Now playing: {currentTrack.music.name}");
        }

        void CheckMusicEnd()
        {
            if (!audioSource.isPlaying && !isTransitioning)
            {
                PlayMusic(currentTrack);
            }
        }
    }
}
