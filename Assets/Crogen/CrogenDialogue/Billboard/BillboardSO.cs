using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue.Billboard
{
	[CreateAssetMenu(fileName = nameof(BillboardSO), menuName = "CrogenDialogue/BillboardSO")]
	[RegisterScript]
    public class BillboardSO : ScriptableObject
    {
		public List<BillboardValueSO> ValueList = new();
		private Dictionary<string, BillboardValueSO> ValueDictionary { get; set; } = new();

		private void OnEnable()
		{
			if (ValueDictionary == null)
				ValueDictionary = new();

			ValueDictionary.Clear();
			foreach (var value in ValueList)
			{
				ValueDictionary.TryAdd(value.Name, value);
			}
		}

#if UNITY_EDITOR
		public BillboardValueSO AddNewValue()
		{
			var valueData = ScriptableObject.CreateInstance(typeof(BillboardValueSO)) as BillboardValueSO;

			ValueList.Add(valueData);
			UnityEditor.AssetDatabase.AddObjectToAsset(valueData, this); // 이러면 SO 하단에 묶임
			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();
			return valueData;
		}

		public void RemoveNode(BillboardValueSO valueSO)
		{
			if (valueSO != null)
			{
				if (ValueList.Contains(valueSO) == false) return;
				ValueList.Remove(valueSO);
			}
			else
			{
				var lastValueData = ValueList[ValueList.Count - 1];
				ValueList.Remove(lastValueData);
			}
		}
#endif

		public int GetIntValue(string name)
		{
			if (ValueDictionary.ContainsKey(name) == false)
			{
				Debug.LogWarning("잘못된 변수명");
				return default;
			}
			if (ValueDictionary[name].ValueType != EBillboardValueType.Int)
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
			if (ValueDictionary[name].ValueType != EBillboardValueType.Float)
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
			if (ValueDictionary[name].ValueType != EBillboardValueType.String)
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
			if (ValueDictionary[name].ValueType != EBillboardValueType.Bool)
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
			if (ValueDictionary[name].ValueType != EBillboardValueType.Int)
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
			if (ValueDictionary[name].ValueType != EBillboardValueType.Float)
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
			if (ValueDictionary[name].ValueType != EBillboardValueType.String)
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
			if (ValueDictionary[name].ValueType != EBillboardValueType.Bool)
			{
				Debug.LogWarning("잘못된 값 타입");
				return;
			}
			ValueDictionary[name].BoolValue = value;
		}
	}
}
