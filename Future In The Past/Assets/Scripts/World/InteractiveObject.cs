using System.Collections.Generic;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class InteractiveObject : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get; private set; }

        private void Start()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public abstract void OnInteract();
    }
}
