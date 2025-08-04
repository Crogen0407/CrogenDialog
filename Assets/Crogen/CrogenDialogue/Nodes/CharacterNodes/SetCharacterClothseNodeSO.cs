using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.CharacterNodes
{
	[CrogenRegisterScript, NodePath("Character")]
	public class SetCharacterClothseNodeSO : EnableCharacterNodeSO
	{
		[field: SerializeField] public string Clothse { get; private set; }

		public override string GetNodeName() => "SetCharacterClothse";
		public override string GetTooltipText() => $"{Character?.Name}의 의상값을 설정합니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.CharacterCotainer.EnableCharacter(Character, string.Empty, Clothse, string.Empty);
			base.Go(storyteller);
		}
	}
}
