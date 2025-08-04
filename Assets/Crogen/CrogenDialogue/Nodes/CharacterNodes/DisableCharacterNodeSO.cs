using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;

namespace Crogen.CrogenDialogue.Nodes.CharacterNodes
{
	[CrogenRegisterScript, NodePath("Character")]
	public class DisableCharacterNodeSO : CharacterNodeSO
	{
		public override string GetNodeName() => "DisableCharacter";
		public override string GetTooltipText() => $"{Character.Name}을(를) 비활성화합니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.CharacterCotainer.DisableCharacter(Character);
			base.Go(storyteller);
		}
	}
}
