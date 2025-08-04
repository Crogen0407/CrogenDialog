using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using System.Collections;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.FlowNodes
{
	[CrogenRegisterScript, NodePath("Flow")]
	public class WaitUntilInputNodeSO : NodeSO
	{
		[field: SerializeField] public KeyCode KeyCode { get; private set; } = KeyCode.Space;

		public override string GetNodeName() => "WaitUntilInput";
		public override string GetTooltipText() => "입력이 감지될 때까지 기다립니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.StartCoroutine(CoroutineWaitUntilInput(storyteller));
		}

		public IEnumerator CoroutineWaitUntilInput(Storyteller storyteller)
		{
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode));
			base.Go(storyteller);
		}

	}
}
