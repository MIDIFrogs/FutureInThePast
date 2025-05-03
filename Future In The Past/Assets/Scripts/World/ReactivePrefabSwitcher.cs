using MIDIFrogs.FutureInThePast.Quests;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
	public class ReactivePrefabSwitcher : ReactiveObject
	{
        [Header("Interaction")]
		[SerializeField] private TriggerConfig trigger;

        [Header("Resources")]
        [SerializeField] private GameObject disablePrefab;
        [SerializeField] private GameObject enablePrefab;

        private GameObject activeObject;

        protected override void Start()
        {
            trigger.Quest.Completed += OnTriggerCompleted;
            trigger.Quest.Restored += OnTriggerRestored;
            base.Start();
            Switch(disablePrefab);
        }

        private void Switch(GameObject nextPrefab)
        {
            if (activeObject != null)
            {
                Destroy(activeObject);
            }
            if (nextPrefab != null)
            {
                activeObject = Instantiate(nextPrefab, transform);
            }
        }

        private void OnTriggerRestored(object sender, System.EventArgs e)
        {
            Switch(disablePrefab);
        }

        private void OnTriggerCompleted(object sender, System.EventArgs e)
        {
            Switch(enablePrefab);
        }
    }
}