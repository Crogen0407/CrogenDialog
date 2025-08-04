using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.FlowNodes
{
	[CrogenRegisterScript, NodePath("Flow")]
	public class GotoNodeSO : NodeSO
	{
		[field: SerializeField] public NodeSO Destination { get; private set; }

		public override string GetNodeName() => "Goto";
		public override string GetTooltipText() => $"{Destination?.name}부터 실행시킵니다.";

		public bool SelectFilter()
		{
			return true;
		}

		public override bool IsWarning() => Destination == null;
		public override string GetWarningText() => "Destination이 null입니다.";

		public override bool IsError() => Destination != null && Destination.StorySO != this.StorySO;
		public override string GetErrorText() => $"\'{Destination.name}\'은(는) {StorySO.name}의 노드가 아닙니다.";

		public override void Go(Storyteller storyteller)
		{
			if (Destination == null)
				for (int i = 0; i < NextNodes.Length; i++)
					NextNodes[i]?.Go(storyteller);
			else
				Destination?.Go(storyteller);
		}
	}
}
