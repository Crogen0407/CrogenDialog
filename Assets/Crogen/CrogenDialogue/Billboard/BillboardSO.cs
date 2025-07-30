using System;
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

		public bool Set<T>(string name, T value) where T : notnull
		{
			if (ValueDictionary.ContainsKey(name) == true)
				if (typeof(T).Equals(ValueDictionary[name].Type) == false)
					return false;

			if (value is int intValue)
				ValueDictionary[name].IntValue = intValue;
			else if (value is float floatValue)
				ValueDictionary[name].FloatValue = floatValue;
			else if (value is bool boolValue)
				ValueDictionary[name].BoolValue = boolValue;
			else if (value is string stringValue)
				ValueDictionary[name].StringValue = stringValue;

			return true;
		}

		public bool Get(string name, out int value)
		{
			if (ValueDictionary.TryGetValue(name, out var entry) && entry.Type == typeof(int))
			{
				value = entry.IntValue;
				return true;
			}
			value = default;
			return false;
		}
		public bool Get(string name, out float value) 
		{
			if (ValueDictionary.TryGetValue(name, out var entry) && entry.Type == typeof(int))
			{
				value = entry.FloatValue;
				return true;
			}
			value = default;
			return false;
		}
		public bool Get(string name, out bool value) 
		{
			if (ValueDictionary.TryGetValue(name, out var entry) && entry.Type == typeof(int))
			{
				value = entry.BoolValue;
				return true;
			}
			value = default;
			return false;
		}
		public bool Get(string name, out string value) 
		{
			if (ValueDictionary.TryGetValue(name, out var entry) && entry.Type == typeof(int))
			{
				value = entry.StringValue;
				return true;
			}
			value = default;
			return false;
		}

	}
}
