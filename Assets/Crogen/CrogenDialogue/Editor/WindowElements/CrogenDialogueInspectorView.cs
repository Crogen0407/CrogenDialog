using Crogen.CrogenDialogue.Billboard;
using Crogen.CrogenDialogue.Editor.UTIL;
using System.Buffers.Text;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor
{
    public class CrogenDialogueInspectorView : VisualElement
    {
		public Dictionary<string, Dictionary<EBillboardValueType, BindableElement>> _billboardFields = new();
		private BillboardValueSO _selectedBillboardValueSO;

		public CrogenDialogueInspectorView Initialize(StorytellerBaseSO storytellerBaseSO)
        {
			// 필드 컨테이너
			{
				var fieldContainer = new VisualElement();
				fieldContainer.name = "fieldContainer";
				fieldContainer.style.minWidth = 250;
				fieldContainer.style.maxWidth = 250;

				DrawBillboardFields(storytellerBaseSO.Billboard, fieldContainer);
				DrawAddAndRemoveButton(storytellerBaseSO.Billboard, fieldContainer);
				Add(fieldContainer);
			}

			return this;
		}

		private void DrawBillboardFields(BillboardSO billboardSO, VisualElement fieldContainer)
		{
			Clear();
			for (int i = 0; i < billboardSO.ValueList.Count; i++)
			{
				var valueElement = billboardSO.ValueList[i];

				// 변수 부모 컨테이너
				VisualElement valueElementContainer = new();
				valueElementContainer.style.flexDirection = FlexDirection.Row;

				// 이름 필드
				{
					TextField nameTextField = new TextField();
					nameTextField.name = "nameTextField";
					nameTextField.isDelayed = true;
					nameTextField.value = valueElement.Name;
					nameTextField.maxLength = 16;
					nameTextField.RegisterValueChangedCallback(evt => {
						valueElement.Name = evt.newValue;
					});
					nameTextField.style.minWidth = 60;
					nameTextField.style.maxWidth = 60;
					valueElementContainer.Add(nameTextField);
				}

				// 타입 필드
				{
					EnumField typeEnumField = new EnumField(valueElement.ValueType);
					typeEnumField.name = "typeEnumField";
					typeEnumField.value = valueElement.ValueType;
					typeEnumField.RegisterValueChangedCallback(evt => {
						valueElement.ValueType = (EBillboardValueType)evt.newValue;
						DrawBillboardFields(billboardSO, fieldContainer);
					});

					typeEnumField.style.minWidth = 60;
					typeEnumField.style.maxWidth = 60;
					valueElementContainer.Add(typeEnumField);
				}

				// 인수값 직렬화 필드
				{
					List<(Type, PropertyField)> propertyFieldTypeList = new();

					// SerializedObject 생성
					SerializedObject soSerialized = new SerializedObject(valueElement);
					var iterator = soSerialized.GetIterator();

					if (iterator.NextVisible(true))
					{
						do
						{
							if (FieldDrawer.IsRenderableField(iterator.name, valueElement.GetType()) == false) continue;

							PropertyField propField = new PropertyField(iterator.Copy());
							propField.style.flexGrow = 1f;
							propField.label = string.Empty;
							propField.Bind(soSerialized);
							propertyFieldTypeList.Add((iterator.GetSystemTypeFromSerializedProperty(), propField));
							valueElementContainer.Add(propField);
						}
						while (iterator.NextVisible(false));
					}

					foreach (var propertyFieldType in propertyFieldTypeList)
					{
						propertyFieldType.Item2.style.display = valueElement.GetValueType() != propertyFieldType.Item1 ? DisplayStyle.None : DisplayStyle.Flex;
					}
				}

				fieldContainer.Add(valueElementContainer);
			}
		}

		private void DrawAddAndRemoveButton(BillboardSO billboardSO, VisualElement fieldContainer)
		{
			var addAndRemoveContainer = new VisualElement();
			addAndRemoveContainer.style.flexDirection = FlexDirection.Row;

			var addButton = new Button();
			addButton.text = "+";
			addButton.style.flexGrow = 0.9f;
			addButton.clicked += () => {
				billboardSO.AddNewValue();
			};
			addAndRemoveContainer.Add(addButton);

			var removeButton = new Button();
			removeButton.text = "-";
			removeButton.style.flexGrow = 0.1f;
			removeButton.clicked += () => {
				billboardSO.RemoveNode(_selectedBillboardValueSO);
			};
			addAndRemoveContainer.Add(removeButton);

			fieldContainer.Add(addAndRemoveContainer);
		}
	}
}
