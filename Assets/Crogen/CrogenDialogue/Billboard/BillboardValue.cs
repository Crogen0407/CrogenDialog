using System;
using UnityEngine;

namespace Crogen.CrogenDialogue.Billboard
{
	[Serializable]
	public class BillboardValue
	{
		[field: SerializeField] public string Name { get; set; }
		[field: SerializeField] public Type Type { get; set; }
		[field: SerializeField] public int IntValue { get; set; }
		[field: SerializeField] public float FloatValue { get; set; }
		[field: SerializeField] public bool BoolValue { get; set; }
		[field: SerializeField] public string StringValue { get; set; }
	}

}
