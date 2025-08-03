namespace Crogen.CrogenDialogue.Nodes.CharacterNodes
{
	public class EnableCharacterNodeSO : CharacterNodeSO
	{
		public override string GetNodeName() => "EnableCharacter";
		public override string GetTooltipText() => $"{Character.Name}을(를) 활성화합니다.";

		public override void Go(Storyteller storyteller)
		{
			storyteller.CharacterCotainer.EnableCharacter(Character);
			base.Go(storyteller);
		}
	}
}
