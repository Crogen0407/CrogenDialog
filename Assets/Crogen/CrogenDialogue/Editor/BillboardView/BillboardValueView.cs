using Crogen.CrogenDialogue.Billboard;
using Crogen.CrogenDialogue.Editor.UTIL;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor
{
    public class BillboardValueView : VisualElement
    {
		private CrogenDialogueBillboardView _billboardView;
        public BillboardValueSO BillboardValueSO { get; private set; }
		private List<(Type, PropertyField)> _typeAndPropertyFieldList = new();

		public BillboardValueView Initialize(BillboardValueSO billboardValueSO, BillboardSO billboardSO, CrogenDialogueBillboardView billboardView)
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

				typeEnumField.style.minWidth = 60;
				typeEnumField.style.maxWidth = 60;
				valueElementContainer.Add(typeEnumField);
			}

			// 이름 필드
			{
				TextField nameTextField = new TextField();
				nameTextField.name = "nameTextField";
				nameTextField.isDelayed = true;
				nameTextField.value = BillboardValueSO.Name;
				nameTextField.maxLength = 16;
				nameTextField.RegisterValueChangedCallback(evt => {
					BillboardValueSO.Name = evt.newValue;
					CheckNameConflict(valueElementContainer, BillboardValueSO, billboardSO);
				});
				nameTextField.style.minWidth = 60;
				nameTextField.style.maxWidth = 60;
				valueElementContainer.Add(nameTextField);
			}

			// 인수값 직렬화 필드
			{
				// SerializedObject 생성
				SerializedObject soSerialized = new SerializedObject(BillboardValueSO);
				var iterator = soSerialized.GetIterator();

				if (iterator.NextVisible(true))
				{
					do
					{
						if (FieldDrawer.IsRenderableField(iterator.name, BillboardValueSO.GetType()) == false) continue;

						PropertyField propField = new PropertyField(iterator.Copy());
						propField.style.flexGrow = 1f;
						propField.label = string.Empty;
						propField.Bind(soSerialized);
						_typeAndPropertyFieldList.Add((iterator.GetSystemTypeFromSerializedProperty(), propField));
						valueElementContainer.Add(propField);
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
				propertyFieldType.Item2.style.display = billboardValueSO.GetValueType() != propertyFieldType.Item1 ? DisplayStyle.None : DisplayStyle.Flex;
		}

		private void CheckNameConflict(VisualElement valueElementContainer, BillboardValueSO billboardValueSO, BillboardSO billboardSO)
		{
			bool isNameDuplicated = false;

			for (int i = 0; i < billboardSO.ValueList.Count; i++)
			{
				if (string.IsNullOrEmpty(billboardValueSO.Name)
					|| (billboardValueSO != billboardSO.ValueList[i] && billboardSO.ValueList[i].Name.Equals(billboardValueSO.Name)))
				{
					isNameDuplicated = true;
					break;
				}
			}

			valueElementContainer.style.backgroundColor = isNameDuplicated == true ? Color.red : ColorPalette.buttonColor;
		}
	}
}
