using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Spike : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerLife>(out var life))
            {
                life.Hit();
                life.RespawnFromCheckpoint();
            }
        }
    }
}
