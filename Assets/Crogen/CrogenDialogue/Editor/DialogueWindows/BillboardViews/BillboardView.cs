using Crogen.CrogenDialogue.Billboard;
using Crogen.CrogenDialogue.Editor.UTIL;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Linq;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor.DialogueWindows.BillboardViews
{
	public class BillboardView : VisualElement
    {
		private BillboardValueSO _selectedBillboardValueSO { get; set; }
		public BillboardValueSO SelectedBillboardValueSO 
		{ 
			get=> _selectedBillboardValueSO;
			set
			{
				_selectedBillboardValueSO = value;

				for (int i = 0; i < _billboardValueViewList.Count; i++)
				{
					_billboardValueViewList[i].style.backgroundColor = 
						value == _billboardValueViewList[i].BillboardValueSO ? ColorPalette.selectedColor : Color.clear;
				}

			}
		}
		private ScrollView fieldContainer;
		private List<BillboardValueView> _billboardValueViewList = new();

		public BillboardView Initialize(StorySO storytellerBaseSO)
        {
			// 타이틀
			{
				Label titleLabel = new();
				titleLabel.name = "titleLabel";
				titleLabel.text = "Billboard";
				Add(titleLabel);
			}

			// 필드 컨테이너
			{
				fieldContainer = new ScrollView();
				fieldContainer.name = "fieldContainer";
				fieldContainer.style.minWidth = 300;
				fieldContainer.style.maxWidth = 300;
				fieldContainer.style.flexGrow = 1;
				fieldContainer.style.flexShrink = 1;
				fieldContainer.style.overflow = Overflow.Visible; // 이건 선택이야. 기본 스크롤만 써도 됨

				style.backgroundColor = ColorPalette.inspectorColor;
				style.borderRightColor = Color.black;
				style.borderRightWidth = 1;

				DrawBillboardFields(storytellerBaseSO.Billboard, fieldContainer);
				DrawAddAndRemoveButton(storytellerBaseSO.Billboard, fieldContainer);
				Add(fieldContainer);
			}

			StyleLoader.AddStyles(this, "CrogenDialogueBillboardView");

			return this;
		}

		private void DrawBillboardFields(BillboardSO billboardSO, VisualElement fieldContainer)
		{
			for (int i = 0; i < billboardSO.ValueList.Count; i++)
			{
				var billboardValueSO = billboardSO.ValueList[i];
				AddPropertyField(billboardValueSO, billboardSO);
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
				AddPropertyField(billboardSO.AddNewValue(), billboardSO);
				addAndRemoveContainer.MoveToBottom(fieldContainer);
			};
			addAndRemoveContainer.Add(addButton);

			var removeButton = new Button();
			removeButton.text = "-";
			removeButton.style.flexGrow = 0.1f;
			removeButton.clicked += () => {
				RemovePropertyField(billboardSO);
			};
			addAndRemoveContainer.Add(removeButton);

			fieldContainer.Add(addAndRemoveContainer);
		}

		private void AddPropertyField(BillboardValueSO billboardValueSO, BillboardSO billboardSO)
		{
			var billboardValueView = new BillboardValueView().Initialize(billboardValueSO, billboardSO, this);
			fieldContainer.contentContainer.Add(billboardValueView);
			_billboardValueViewList.Add(billboardValueView);
		}
		private void RemovePropertyField(BillboardSO billboardSO)
		{
			var removedBillboardValueSO = SelectedBillboardValueSO != null ? SelectedBillboardValueSO : billboardSO.GetLastValueSO();
			fieldContainer.contentContainer.Remove(_billboardValueViewList.FirstOrDefault(x => x.BillboardValueSO == removedBillboardValueSO));
			billboardSO.RemoveNode(SelectedBillboardValueSO);
		}
	}
}