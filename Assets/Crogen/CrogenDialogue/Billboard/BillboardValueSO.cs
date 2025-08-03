using System;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenDialogue.Billboard
{
	[RegisterScript]
	public class BillboardValueSO : ScriptableObject
	{
		[field: SerializeField, HideInEditorWindow] public string Name { get; set; } = string.Empty;
		[field: SerializeField, HideInEditorWindow] public EBillboardValueType ValueType { get; set; }
		[field: SerializeField] public int IntValue { get; set; }
		[field: SerializeField] public float FloatValue { get; set; }
		[field: SerializeField] public bool BoolValue { get; set; }
		[field: SerializeField] public string StringValue { get; set; }

		public Type GetValueType() 
			=> GetValueType(ValueType);

		public static Type GetValueType(EBillboardValueType valueType)
		{
			switch (valueType)
			{
				case EBillboardValueType.Int:
					return typeof(int);
				case EBillboardValueType.Float:
					return typeof(float);
				case EBillboardValueType.Bool:
					return typeof(bool);
				case EBillboardValueType.String:
					return typeof(string);
			}

			return null;
		}
	}
}
