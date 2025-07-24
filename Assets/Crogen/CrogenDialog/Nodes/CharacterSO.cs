using UnityEngine;

namespace Crogen.CrogenDialog
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
