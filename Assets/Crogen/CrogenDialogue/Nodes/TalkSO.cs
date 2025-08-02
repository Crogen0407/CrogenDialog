using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterNode]
	public class TalkSO : NodeSO
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public string Talk { get; private set; }

		public override string GetNodeName() =>"Talk";
		public override string GetTooltipText() => "대화창 UI를 컨트롤합니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.SetTalk(Name, Talk);
			base.Go(storyteller);
		}
	}
}
