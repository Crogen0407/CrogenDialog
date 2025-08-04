using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using System.Collections;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.TalkNodes
{
	[CrogenRegisterScript, NodePath("Talk")]
	public class TalkNodeSO : NodeSO
	{
		[TextArea(1, 1), Delayed] public string nameText;
		[TextArea, Delayed] public string talkText;

		[field: SerializeField] public KeyCode KeyCode { get; private set; } = KeyCode.Space;

		public override string GetNodeName() =>"Talk";
		public override string GetTooltipText() => "대화창 UI를 컨트롤합니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.TalkContainer.SetTalkText(nameText, talkText);
			storyteller.StartCoroutine(CoroutineNextGo(storyteller));
		}

		private IEnumerator CoroutineNextGo(Storyteller storyteller)
		{
			yield return null;
			yield return new WaitUntil(() => storyteller.IsTalkComplete || Input.GetKeyDown(KeyCode));
			storyteller.IsTalkComplete = true;

			yield return null;
			yield return new WaitUntil(() => Input.GetKeyDown(KeyCode));
			if (NextNodes[0] != null && NextNodes[0] is not TalkNodeSO)
				storyteller.TalkContainer.SetActive(false);
			base.Go(storyteller);
		}
	}
}
