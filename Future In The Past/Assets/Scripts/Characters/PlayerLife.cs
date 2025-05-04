using MIDIFrogs.FutureInThePast.Quests;
using MIDIFrogs.FutureInThePast.Navigation;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class PlayerLife : MonoBehaviour
    {
        [SerializeField] private SimpleNavigator endingNavigator;
        [SerializeField] private int maxHp;
        [SerializeField] private Transform initialCheckpoint;
        [SerializeField] private TriggerConfig deathTrigger;

        private Transform lastCheckpoint;

        public int Health { get; private set; }

        private void Start()
        {
            Health = maxHp;
            lastCheckpoint = initialCheckpoint;
            deathTrigger.Quest.Completed += (s, e) => Kill();
        }

        public void Hit()
        {
            Health--;
            if (Health <= 0)
            {
                Kill();
            }
        }

        public void RespawnFromCheckpoint()
        {
            transform.position = lastCheckpoint.position;
        }

        public void Kill()
        {
            QuestManager.SetTrigger("Death");
            EndGame();
        }

        public void EndGame()
        {
            endingNavigator.Navigate();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Checkpoint"))
            {
                lastCheckpoint = collision.transform;
            }
        }
    }
}