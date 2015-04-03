using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

namespace AssemblyCSharp
{
	[Serializable]
	public class TheseDialogQueuers
	{
		private DialogQueuer thisDialogQueuer;
		private bool hasBeenQueued = false;
		public bool isKey = false;
		public int lettersPerSecond = 15;
		public MonoBehaviour endedResponse;
		public LineAndSpeaker[] lines;


		public bool getHasBeenQueued()
		{
			return hasBeenQueued;
		}

		public void setHasBeenQueued(bool value)
		{
			hasBeenQueued = value;
		}

		public DialogQueuer getThisDialogQueuer()
		{
			return thisDialogQueuer;
		}

		public void setThisDialogQueuer(DialogQueuer d)
		{
			thisDialogQueuer = d;
		}
	}

	public class DialogForks : ButtonAction
	{
		public TheseDialogQueuers[] dialogQueuers;
		public CharacterToMaterial[] characterToMaterialMapping;
		public GameObject chatWindow;

		void Start()
		{
			// Populate variables
			foreach(TheseDialogQueuers d in dialogQueuers)
			{
				DialogQueuer temp = this.gameObject.AddComponent<DialogQueuer>();
				temp.lines = d.lines;
				temp.lettersPerSecond = d.lettersPerSecond;
				temp.endedResponse = d.endedResponse;
				temp.characterToMaterialMapping = characterToMaterialMapping;
				temp.chatWindow = chatWindow;

				d.setThisDialogQueuer(temp);
			}
		}

		public override void takeAction ()
		{
			foreach(TheseDialogQueuers d in dialogQueuers)
			{
				d.getThisDialogQueuer().takeAction();
				if (d.isKey) 
				{
					DataHolder.keysFound++;

					Debug.Log(DataHolder.keysFound);
				}
			}
			 
		}

	}
}

