using Crogen.CrogenDialogue.Nodes;
using UnityEngine;

namespace Crogen.CrogenDialogue.NodeBlocks
{
	[RegisterNode]
	public abstract class NodeBlockSO : ScriptableObject
    {
        public abstract void Go(GeneralNodeSO parentNode);
    }
}
