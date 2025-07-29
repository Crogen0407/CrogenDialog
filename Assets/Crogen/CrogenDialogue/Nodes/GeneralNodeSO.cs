using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[RegisterNode]
	public class GeneralNodeSO : ScriptableObject
	{
		[field: SerializeField, HideInEditorWindow] public Vector2 Position { get; set; }

		public virtual string[] GetOutputPortsNames() => new[] { string.Empty };
		public virtual string GetNodeName() => "GeneralNode";
		public virtual int GetOutputPortCount()=> 1;

		public virtual void Go(CrogenDialogueManager crogenDialogueManager, Storyteller storyteller) { }
	}
}