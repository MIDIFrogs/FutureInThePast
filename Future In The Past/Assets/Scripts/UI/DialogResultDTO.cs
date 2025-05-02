using System;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.UI
{
    [Serializable]
	[CreateAssetMenu(fileName = "New Dialog Result", menuName = "Tools/Dialog result")]
	public class DialogResultDTO : ScriptableObject
	{
		public DialogResult result;
	}
}