using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[CrogenRegisterScript]
	public class TalkSO : NodeSO
	{
		[field: SerializeField] public string Name { get; private set; }
		[TextArea, Delayed] public string talk;

		public override string GetNodeName() =>"Talk";
		public override string GetTooltipText() => "대화창 UI를 컨트롤합니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.SetTalk(Name, talk);
			base.Go(storyteller);
		}
	}
}
