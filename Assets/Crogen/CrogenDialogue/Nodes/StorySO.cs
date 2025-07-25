using UnityEngine;

namespace Crogen.CrogenDialogue
{
    [CreateAssetMenu(fileName = "StorySO", menuName = "Scriptable Objects/StorySO")]
	public class StorySO : NodeSO
	{
        [field:SerializeField] public NodeSO StartNode { get; set; }

		public override string GetNodeName()
		{
			return "Story";
		}
	}
}
