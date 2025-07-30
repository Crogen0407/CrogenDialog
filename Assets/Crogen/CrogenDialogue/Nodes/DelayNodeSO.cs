using System.Collections;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterNode]
	public class DelayNodeSO : GeneralNodeSO
	{
		[field: SerializeField] public float Delay { get; private set; } = 0.1f;

		public override string GetNodeName() => "Delay";

		public override string GetTooltip() => $"{Delay}�� ���� ��ٸ����� ���� ��带 �����մϴ�.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.StartCoroutine(DelayNextGo(storyteller, Delay));
		}

		private IEnumerator DelayNextGo(Storyteller storyteller, float delay)
		{
			yield return new WaitForSeconds(delay);

			for (int i = 0; i < NodeBlockList.Count; i++)
				NodeBlockList[i]?.Go(this);

			for (int i = 0; i < NextNodes.Length; i++)
				NextNodes[i]?.Go(storyteller);
		}
	}
}
