using Crogen.CrogenDialogue.Character;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes.CharacterNodes
{
    public abstract class CharacterNodeSO : NodeSO
    {
		[field: SerializeField] public CharacterSO Character { get; set; }
	}
}
