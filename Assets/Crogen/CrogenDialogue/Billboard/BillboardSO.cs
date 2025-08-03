using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue.Billboard
{
	[CreateAssetMenu(fileName = nameof(BillboardSO), menuName = "CrogenDialogue/BillboardSO")]
	[CrogenRegisterScript]
    public class BillboardSO : ScriptableObject
    {
		public List<BillboardValueSO> ValueList = new();
		private Dictionary<string, BillboardValueSO> ValueDictionary { get; set; } = new();

		public int Count => ValueList.Count;
		public BillboardValueSO this[int index]
			=> ValueList[index];

		private void OnEnable()
		{
			if (ValueDictionary == null)
				ValueDictionary = new();

			ValueDictionary.Clear();
			foreach (var value in ValueList)
			{
				ValueDictionary.TryAdd(value.name, value);
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
			BillboardValueSO removedValueSO = valueSO != null ? valueSO : GetLastValueSO();
			ValueList.Remove(removedValueSO);

			UnityEditor.AssetDatabase.RemoveObjectFromAsset(removedValueSO);
			DestroyImmediate(removedValueSO, true); // 완전히 메모리에서 제거
			UnityEditor.AssetDatabase.SaveAssets();

			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();
		}

		public BillboardValueSO GetLastValueSO() 
			=> ValueList[ValueList.Count - 1];
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
			return ValueDictionary[name].BillBoardValues.IntValue;
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
			return ValueDictionary[name].BillBoardValues.FloatValue;
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
			return ValueDictionary[name].BillBoardValues.StringValue;
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
			return ValueDictionary[name].BillBoardValues.BoolValue;
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
			ValueDictionary[name].BillBoardValues.IntValue = value;
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
			ValueDictionary[name].BillBoardValues.FloatValue = value;
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
			ValueDictionary[name].BillBoardValues.StringValue = value;
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
			ValueDictionary[name].BillBoardValues.BoolValue = value;
		}
	}
}
