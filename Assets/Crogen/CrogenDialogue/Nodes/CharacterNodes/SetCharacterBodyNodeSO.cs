using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.CharacterNodes
{
	[CrogenRegisterScript, NodePath("Character")]
	public class SetCharacterBodyNodeSO : EnableCharacterNodeSO
	{
		[field: SerializeField] public string Body { get; private set; }

		public override string GetNodeName() => "SetCharacterBody";
		public override string GetTooltipText() => $"{Character?.Name}의 바디값을 설정합니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.CharacterCotainer.EnableCharacter(Character, Body, string.Empty, string.Empty);
			base.Go(storyteller);
		}
	}
}
