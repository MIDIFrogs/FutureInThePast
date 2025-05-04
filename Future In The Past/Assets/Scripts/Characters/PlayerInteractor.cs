namespace FutureInThePast.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using MIDIFrogs.FutureInThePast;
    using UnityEngine;

    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private Material outlineMaterial;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private GameObject hint;

        private readonly HashSet<InteractiveObject> interactives = new();

        private void Update()
        {
            foreach (var interactive in interactives)
            {
                interactive.SpriteRenderer.material = defaultMaterial;
            }
            var nearestInteractable = interactives.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).FirstOrDefault();
            if (nearestInteractable != null)
            {
                nearestInteractable.SpriteRenderer.material = outlineMaterial;
                hint.transform.position = (Vector2)(nearestInteractable.transform.position + Vector3.up);
                hint.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    foreach (var x in nearestInteractable.GetComponents<InteractiveObject>()) x.OnInteract();
                }
            }
            else
            {
                hint.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<InteractiveObject>(out var interactive))
            {
                
                interactives.Add(interactive);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<InteractiveObject>(out var interactive))
            {
                
                interactives.Remove(interactive);
            }
        }
    }
}
