namespace Crogen.CrogenDialogue.Nodes.CharacterNodes
{
	public class SetCharacterFaceNodeSO : EnableCharacterNodeSO
	{
		public override string GetNodeName() => "SetCharacterFace";
		public override string GetTooltipText() => $"{Character.Name}의 표정값을 설정합니다.";

		public override void Go(Storyteller storyteller)
		{

			base.Go(storyteller);
		}
	}
}
