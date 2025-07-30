using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterNode]
	public class TalkSO : GeneralNodeSO
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public string Talk { get; private set; }

		public override string GetNodeName() =>"Talk";
	}
}
