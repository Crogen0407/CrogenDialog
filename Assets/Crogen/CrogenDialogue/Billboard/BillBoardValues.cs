using System;
using UnityEngine;

namespace Crogen.CrogenDialogue.Billboard
{
	[Serializable]
	public enum EBillboardValueType
	{
		Int, 
		Float,
		Bool,
		String,
		Vector2,
		Vector3,
	}

	[Serializable]
	public class BillBoardValues : ICloneable
    {
		[field: SerializeField] public int IntValue { get; set; }
		[field: SerializeField] public float FloatValue { get; set; }
		[field: SerializeField] public bool BoolValue { get; set; }
		[field: SerializeField] public string StringValue { get; set; }
		[field: SerializeField] public Vector2 Vector2Value { get; set; }
		[field: SerializeField] public Vector3 Vector3Value { get; set; }

		public bool Equal(EBillboardValueType valueType, BillBoardValues otherValues)
		{
			switch (valueType)
			{
				case EBillboardValueType.Int:
					return IntValue == otherValues.IntValue;
				case EBillboardValueType.Float:
					return FloatValue == otherValues.FloatValue;
				case EBillboardValueType.Bool:
					return BoolValue == otherValues.BoolValue;
				case EBillboardValueType.String:
					return StringValue == otherValues.StringValue;
				case EBillboardValueType.Vector2:
					return Vector2Value == otherValues.Vector2Value;
				case EBillboardValueType.Vector3:
					return Vector3Value == otherValues.Vector3Value;
			}

			return false;
		}

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
				case EBillboardValueType.Vector2:
					return typeof(Vector2);
				case EBillboardValueType.Vector3:
					return typeof(Vector3);
			}

			return null;
		}

		public object Clone()
		{
			return new BillBoardValues()
			{
				IntValue = this.IntValue,
				FloatValue = this.FloatValue,
				StringValue = this.StringValue,
				BoolValue = this.BoolValue,
				Vector2Value = this.Vector2Value,
				Vector3Value = this.Vector3Value
			};
		}

	}
}
