using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class InteractableObject : MonoBehaviour
    {
        [Header("VFX")]
        [SerializeField] private Material outlineMaterial;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private GameObject interactionHint;

        private SpriteRenderer sprite;

        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnInteract();
            }
        }

        protected abstract void OnInteract();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                sprite.material = outlineMaterial;
                interactionHint.transform.position = (Vector2)(transform.position + Vector3.up);
                interactionHint.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                sprite.material = defaultMaterial;
                interactionHint.SetActive(false);
            }
        }
    }
}
