using System.Collections;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterNode]
	public class DelayNodeSO : GeneralNodeSO
	{
		[field: SerializeField] public float Delay { get; private set; } = 0.1f;

		public override string GetNodeName() => "Delay";

		public override string GetTooltip() => $"{Delay}초 동안 기다리고나서 다음 노드를 실행합니다.";

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
