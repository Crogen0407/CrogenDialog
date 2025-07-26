using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[RegisterNode]
	[CreateAssetMenu(fileName = nameof(ValueBillboardSO), menuName = "CrogenDialogue/ValueBillboardSO")]
	public class ValueBillboardSO : NodeSO
	{
		[field: SerializeField] public string ValueName { get; private set; }

		public override string GetNodeName()
		{
			return "ValueBillboard";
		}
	}
}
