using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[CreateAssetMenu(fileName = "CharacterSO", menuName = "Scriptable Objects/CharacterSO")]
	public class CharacterSO : NodeSO
	{
		public override string GetNodeName()
		{
			return "Character";
		}
	}
}
