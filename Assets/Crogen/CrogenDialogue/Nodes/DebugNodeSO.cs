using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterNode]
	public class DebugNodeSO : GeneralNodeSO
	{
		public override string GetNodeName() => "Debug";

		[TextArea] public string message;

		public override void Go(Storyteller storyteller)
		{
			Debug.Log(message);
			base.Go(storyteller);
		}
	}
}
