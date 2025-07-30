using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterNode]
	public class ConditionalNodeSO : GeneralNodeSO
	{
		public override int GetOutputPortCount() => 2;

		public override string[] GetOutputPortsNames()
		{
			return new[] { "True", "False" };
		}

		public override string GetNodeName() => "Conditional";
	}
}
