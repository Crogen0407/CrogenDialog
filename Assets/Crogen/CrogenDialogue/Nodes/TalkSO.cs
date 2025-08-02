using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterNode]
	public class TalkSO : NodeSO
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public string Talk { get; private set; }

		public override string GetNodeName() =>"Talk";
		public override string GetTooltipText() => "��ȭâ UI�� ��Ʈ���մϴ�.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.SetTalk(Name, Talk);
			base.Go(storyteller);
		}
	}
}
