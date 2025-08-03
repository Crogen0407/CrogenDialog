using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[CrogenRegisterScript]
	public class TalkSO : NodeSO
	{
		[field: SerializeField] public string Name { get; private set; }
		[TextArea, Delayed] public string talk;

		public override string GetNodeName() =>"Talk";
		public override string GetTooltipText() => "��ȭâ UI�� ��Ʈ���մϴ�.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.SetTalk(Name, talk);
			base.Go(storyteller);
		}
	}
}
