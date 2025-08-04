using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.CharacterNodes
{
	[CrogenRegisterScript, NodePath("Character")]
	public class EnableCharacterNodeSO : CharacterNodeSO
	{
		public override string GetNodeName() => "EnableCharacter";
		public override string GetTooltipText() => $"{Character?.Name}을(를) 활성화합니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.CharacterCotainer.EnableCharacter(Character, string.Empty, string.Empty, string.Empty);
			base.Go(storyteller);
		}
	}
}
