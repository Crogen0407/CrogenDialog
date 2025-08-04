using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using System.Collections;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.TalkNodes
{
	[CrogenRegisterScript, NodePath("Talk")]
	public class ChoiceNodeSO : NodeSO
	{
		[field: SerializeField] public string[] _choices = new string[1];

		public override string GetNodeName() => "Choice";
		public override string GetTooltipText() => "선택지 목록을 셋팅합니다.";

		protected override void OnValidate()
		{
			if (_choices.Length != NextNodes.Length)
			{
				var newNextNodes = new NodeSO[_choices.Length];
				for (int i = 0; i < Mathf.Min(newNextNodes.Length, NextNodes.Length); i++)
				{
					newNextNodes[i] = NextNodes[i];
				}

				NextNodes = newNextNodes;
			}
			base.OnValidate();
		}

		public override int GetOutputPortCount() => _choices.Length;

		public override string[] GetOutputPortsNames()
		{
			var portNames = new string[_choices.Length];

			for (int i = 0; i < _choices.Length; i++)
			{
				portNames[i] = string.Empty;
			}

			return portNames;
		}

		public override void Go(Storyteller storyteller)
		{
			storyteller.ChoiceContainer.SetChoices(_choices);
			storyteller.StartCoroutine(CoroutineNextGo(storyteller));
		}

		private IEnumerator CoroutineNextGo(Storyteller storyteller)
		{
			yield return null;
			yield return new WaitUntil(() => storyteller.ChoiceContainer.IsChoiceComplete);
			NextNodes[storyteller.ChoiceIndex]?.Go(storyteller);
		}
	}
}
