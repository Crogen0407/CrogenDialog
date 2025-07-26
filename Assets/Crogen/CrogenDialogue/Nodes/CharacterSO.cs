using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[RegisterNode]
	public class CharacterSO : NodeSO
	{
		public string Name;
		public string Description;
		public Sprite Sprite;
		public LayerMask layerMask;

		public override string GetNodeName()
		{
			return "Character";
		}
	}
}
