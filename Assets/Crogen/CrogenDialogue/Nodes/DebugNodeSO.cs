using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterNode]
	public class DebugNodeSO : GeneralNodeSO
	{
		[TextArea] public string message;

		public override string GetNodeName() => "Debug";
		public override string GetTooltip() => "디버깅용 노드입니다.";

		public override void Go(Storyteller storyteller)
		{
			Debug.Log(message);
			base.Go(storyteller);
		}
	}
}
