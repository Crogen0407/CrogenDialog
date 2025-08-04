using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using System.Collections;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.FlowNodes
{
	[CrogenRegisterScript, NodePath("Flow")]
	public class DelayNodeSO : NodeSO
	{
		[field: SerializeField] public float Delay { get; private set; } = 0.1f;

		public override string GetNodeName() => "Delay";

		public override string GetTooltipText() => $"{Delay}초 동안 기다리고나서 다음 노드를 실행합니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.StartCoroutine(DelayNextGo(storyteller, Delay));
		}

		private IEnumerator DelayNextGo(Storyteller storyteller, float delay)
		{
			yield return new WaitForSeconds(delay);
			base.Go(storyteller);
		}
	}
}
