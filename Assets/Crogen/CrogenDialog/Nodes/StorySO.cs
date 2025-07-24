using UnityEngine;

namespace Crogen.CrogenDialog
{
    [CreateAssetMenu(fileName = "StorySO", menuName = "Scriptable Objects/StorySO")]
    public class StorySO : NodeSO
	{
        [field:SerializeField] public NodeSO StartNode { get; set; }
    }
}
