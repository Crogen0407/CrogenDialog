using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[RegisterNode]
	public class TalkSO : NodeSO
	{
		public TalkSO()
		{
			AddInput(null);
			AddOutput(null);
		}

		public override string GetNodeName()
		{
			return "Talk";
		}
	}
}
