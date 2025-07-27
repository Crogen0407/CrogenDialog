using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[RegisterNode]
	public class CharacterSO : GeneralNodeSO
	{
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public string Description { get; private set; }
		[field: SerializeField] public Sprite Sprite { get; private set; }
		[field: SerializeField] public LayerMask layerMask { get; private set; }

		public override string GetNodeName()
		{
			return "Character";
		}
	}
}
