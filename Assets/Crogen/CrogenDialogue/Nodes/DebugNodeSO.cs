using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[CrogenRegisterScript] // 기본 노드라는 뜻. 사용자는 이거 붙힐 필요없음
	public class DebugNodeSO : NodeSO
	{
		[TextArea, Delayed] public string message;

		public override string GetNodeName() => "Debug";
		public override string GetTooltipText() => "디버깅용 노드입니다.";

		public override void Go(Storyteller storyteller)
		{
			Debug.Log(message);
			base.Go(storyteller);
		}
	}
}