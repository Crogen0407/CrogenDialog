using Crogen.CrogenDialogue.Billboard;
using Crogen.CrogenDialogue.Editor.UTIL;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor.DialogueWindows.BillboardViews
{
    public class BillboardValueView : VisualElement
    {
		private BillboardView _billboardView;
        public BillboardValueSO BillboardValueSO { get; private set; }
		private List<(Type, PropertyField)> _typeAndPropertyFieldList = new();

		public BillboardValueView Initialize(BillboardValueSO billboardValueSO, BillboardSO billboardSO, BillboardView billboardView)
        {
            this.BillboardValueSO = billboardValueSO;
			this._billboardView = billboardView;
			
			// 변수 부모 컨테이너
			Button valueElementContainer = new();
			valueElementContainer.clicked += () => {
				_billboardView.SelectedBillboardValueSO = BillboardValueSO;
			};

			valueElementContainer.style.flexDirection = FlexDirection.Row;

			// 타입 필드
			{
				EnumField typeEnumField = new EnumField(billboardValueSO.ValueType);
				typeEnumField.name = "typeEnumField";
				typeEnumField.value = BillboardValueSO.ValueType;
				typeEnumField.RegisterValueChangedCallback(evt => {
					BillboardValueSO.ValueType = (EBillboardValueType)evt.newValue;
					CheckValueDisplay(BillboardValueSO);
				});

				typeEnumField.style.minWidth = 70;
				typeEnumField.style.maxWidth = 70;
				valueElementContainer.Add(typeEnumField);
			}

			// 이름 필드
			{
				TextField nameTextField = new TextField();
				nameTextField.name = "nameTextField";
				nameTextField.isDelayed = true;
				nameTextField.value = BillboardValueSO.name;
				nameTextField.maxLength = 16;
				nameTextField.RegisterValueChangedCallback(evt => {
					BillboardValueSO.name = evt.newValue;
					EditorUtility.SetDirty(BillboardValueSO);
					AssetDatabase.SaveAssets();
					CheckNameConflict(valueElementContainer, BillboardValueSO, billboardSO);
				});
				nameTextField.style.minWidth = 60;
				nameTextField.style.maxWidth = 60;
				valueElementContainer.Add(nameTextField);
			}

			// 인수값 직렬화 필드
			{
				SerializedObject soSerialized = new SerializedObject(BillboardValueSO);
				SerializedProperty iterator = soSerialized.GetIterator();

				if (iterator.NextVisible(true))
				{
					do
					{
						// DefaultValues 안의 개별 필드들을 처리
						if (iterator.name == "<BillBoardValues>k__BackingField")
						{
							var defaultValues = iterator.Copy();

							SerializedProperty innerProp = defaultValues.Copy();
							SerializedProperty endProp = defaultValues.GetEndProperty();

							while (innerProp.NextVisible(true) && !SerializedProperty.EqualContents(innerProp, endProp))
							{
								if (FieldDrawer.IsRenderableField(innerProp.name, innerProp.serializedObject.targetObject.GetType()) == false)
									continue;

								var propField = new PropertyField(innerProp.Copy())
								{
									style = { flexGrow = 1f },
									label = string.Empty
								};
								propField.Bind(soSerialized);
								_typeAndPropertyFieldList.Add((SerializedPropertyExtension.GetSystemTypeFromSerializedProperty(innerProp), propField));
								valueElementContainer.Add(propField);
							}

							break;
						}
					}
					while (iterator.NextVisible(false));
				}

				CheckValueDisplay(billboardValueSO);
			}


			Add(valueElementContainer);
			CheckNameConflict(valueElementContainer, billboardValueSO, billboardSO);

			return this;
		}

		private void CheckValueDisplay(BillboardValueSO billboardValueSO)
		{
			foreach (var propertyFieldType in _typeAndPropertyFieldList)
				propertyFieldType.Item2.style.display = BillBoardValues.GetValueType(billboardValueSO.ValueType) != propertyFieldType.Item1 ? DisplayStyle.None : DisplayStyle.Flex;
		}

		private void CheckNameConflict(VisualElement valueElementContainer, BillboardValueSO billboardValueSO, BillboardSO billboardSO)
		{
			bool isNameDuplicated = false;

			for (int i = 0; i < billboardSO.ValueList.Count; i++)
			{
				if (string.IsNullOrEmpty(billboardValueSO.name)
					|| (billboardValueSO != billboardSO.ValueList[i] && billboardSO.ValueList[i].name.Equals(billboardValueSO.name)))
				{
					isNameDuplicated = true;
					break;
				}
			}

			valueElementContainer.style.backgroundColor = isNameDuplicated == true ? Color.red : ColorPalette.buttonColor;
		}
	}
}
