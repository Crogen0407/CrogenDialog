using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.CharacterNodes
{
	[CrogenRegisterScript, NodePath("Character")]
	public class SetCharacterFaceNodeSO : EnableCharacterNodeSO
	{
		[field: SerializeField] public string Face { get; private set; }

		public override string GetNodeName() => "SetCharacterFace";
		public override string GetTooltipText() => $"{Character?.Name}의 표정값을 설정합니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.CharacterCotainer.EnableCharacter(Character, string.Empty, string.Empty, Face);
			base.Go(storyteller);
		}
	}
}
