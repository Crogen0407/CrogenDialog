using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using Crogen.CrogenDialogue.Billboard;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.BillboardNodes
{
	[CrogenRegisterScript, NodePath("Billboard")]
	public class SetterNodeSO : NodeSO
	{
		[field: SerializeField] public BillboardValueSO TargetValueSO { get; private set; }
		[field: SerializeField] public BillBoardValues NewValue { get; private set; }

		public override string GetNodeName() => "Setter";
		public override string GetTooltipText() => "TargetValueSO의 값을 NewValue로 수정합니다.";

		public override void Go(Storyteller storyteller)
		{
			TargetValueSO.BillBoardValues = NewValue.Clone() as BillBoardValues;

			base.Go(storyteller);
		}
	}
}
