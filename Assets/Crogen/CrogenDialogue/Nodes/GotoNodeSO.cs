using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterNode]
	public class GotoNodeSO : GeneralNodeSO
	{
		[field: SerializeField] public GeneralNodeSO Destination { get; private set; }

		public override string GetNodeName() => "Goto";
		public override string GetTooltip() => $"{Destination?.name}부터 실행시킵니다.";

		public override void Go(Storyteller storyteller)
		{
			for (int i = 0; i < NodeBlockList.Count; i++)
				NodeBlockList[i]?.Go(this);

			if (Destination == null)
				for (int i = 0; i < NextNodes.Length; i++)
					NextNodes[i]?.Go(storyteller);
			else
				Destination?.Go(storyteller);
		}
	}
}
