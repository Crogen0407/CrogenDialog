using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue.Billboard
{
	[CreateAssetMenu(fileName = nameof(BillboardSO), menuName = "CrogenDialogue/BillboardSO")]
    public class BillboardSO : ScriptableObject
    {
		public List<BillboardValue> ValueList = new();
		[field: SerializeField] public Dictionary<string, BillboardValue> ValueDictionary { get; private set; }

		private void OnEnable()
		{
			ValueDictionary = new();
			foreach (var value in ValueList)
			{
				ValueDictionary.Add(value.Name, value);
			}
		}

		public int GetIntValue(string name)
		{
			if (ValueDictionary.ContainsKey(name) == false)
			{
				Debug.LogWarning("잘못된 변수명");
				return default;
			}
			if (ValueDictionary[name].Type != EBillboardValueType.Int)
			{
				Debug.LogWarning("잘못된 값 타입");
				return default;
			}
			return ValueDictionary[name].IntValue;
		}
		public float GetFloatValue(string name)
		{
			if (ValueDictionary.ContainsKey(name) == false)
			{
				Debug.LogWarning("잘못된 변수명");
				return default;
			}
			if (ValueDictionary[name].Type != EBillboardValueType.Float)
			{
				Debug.LogWarning("잘못된 값 타입");
				return default;
			}
			return ValueDictionary[name].FloatValue;
		}
		public string GetStringValue(string name)
		{
			if (ValueDictionary.ContainsKey(name) == false)
			{
				Debug.LogWarning("잘못된 변수명");
				return default;
			}
			if (ValueDictionary[name].Type != EBillboardValueType.String)
			{
				Debug.LogWarning("잘못된 값 타입");
				return default;
			}
			return ValueDictionary[name].StringValue;
		}
		public bool GetBoolValue(string name)
		{
			if (ValueDictionary.ContainsKey(name) == false)
			{
				Debug.LogWarning("잘못된 변수명");
				return default;
			}
			if (ValueDictionary[name].Type != EBillboardValueType.Bool)
			{
				Debug.LogWarning("잘못된 값 타입");
				return default;
			}
			return ValueDictionary[name].BoolValue;
		}

		public void SetIntValue(int value)
		{
			if (ValueDictionary.ContainsKey(name) == false)
			{
				Debug.LogWarning("잘못된 변수명");
				return;
			}
			if (ValueDictionary[name].Type != EBillboardValueType.Int)
			{
				Debug.LogWarning("잘못된 값 타입");
				return;
			}
			ValueDictionary[name].IntValue = value;
		}
		public void SetFloatValue(float value)
		{
			if (ValueDictionary.ContainsKey(name) == false)
			{
				Debug.LogWarning("잘못된 변수명");
				return;
			}
			if (ValueDictionary[name].Type != EBillboardValueType.Float)
			{
				Debug.LogWarning("잘못된 값 타입");
				return;
			}
			ValueDictionary[name].FloatValue = value;
		}
		public void SetStringValue(string value)
		{
			if (ValueDictionary.ContainsKey(name) == false)
			{
				Debug.LogWarning("잘못된 변수명");
				return;
			}
			if (ValueDictionary[name].Type != EBillboardValueType.String)
			{
				Debug.LogWarning("잘못된 값 타입");
				return;
			}
			ValueDictionary[name].StringValue = value;
		}
		public void SetBoolValue(bool value)
		{
			if (ValueDictionary.ContainsKey(name) == false)
			{
				Debug.LogWarning("잘못된 변수명");
				return;
			}
			if (ValueDictionary[name].Type != EBillboardValueType.Bool)
			{
				Debug.LogWarning("잘못된 값 타입");
				return;
			}
			ValueDictionary[name].BoolValue = value;
		}
	}
}
