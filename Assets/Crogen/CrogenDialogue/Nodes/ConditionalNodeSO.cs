using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterScript]
	public class ConditionalNodeSO : NodeSO
	{
		public override int GetOutputPortCount() => 2;

		public override string[] GetOutputPortsNames() => new[] { "True", "False" };
		public override string GetNodeName() => "Conditional";
		public override string GetTooltipText() => "조건이 충족함에 따라 True, False쪽을 실행합니다.";
	}
}
