using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[RegisterNode]
	public class GeneralNodeSO : ScriptableObject
	{
		[field: SerializeField, HideInEditorWindow] public GeneralNodeSO[] NextNodes;
		[field: SerializeField, HideInEditorWindow] public Vector2 Position { get; set; }

		public virtual string[] GetOutputPortsNames() => new[] { string.Empty };
		public virtual string GetNodeName() => "GeneralNode";
		public virtual int GetOutputPortCount()=> 1;

		protected virtual void OnEnable()
		{
			if (NextNodes == null) 
				NextNodes = new GeneralNodeSO[GetOutputPortCount()];
		}

		public virtual GeneralNodeSO[] Go(Storyteller storyteller)
		{
			return NextNodes;
		}
	}
}