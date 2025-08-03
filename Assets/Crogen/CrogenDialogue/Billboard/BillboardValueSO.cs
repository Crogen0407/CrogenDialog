using System;
using UnityEngine;

namespace Crogen.CrogenDialogue.Billboard
{
	[CrogenRegisterScript]
	public class BillboardValueSO : ScriptableObject
	{
		[field: SerializeField, HideInEditorWindow] public string Name { get; set; } = string.Empty;
		[field: SerializeField, HideInEditorWindow] public EBillboardValueType ValueType { get; set; }
		[field: SerializeField] public BillBoardValues BillBoardValues { get; set; }
		private BillBoardValues DefaultValues { get; set; }

		public void SaveDefaultValues()
		{
			DefaultValues = BillBoardValues.Clone() as BillBoardValues;
		}

		public void ReturnToDefault()
		{
			BillBoardValues = DefaultValues.Clone() as BillBoardValues;
		}
	}
}
