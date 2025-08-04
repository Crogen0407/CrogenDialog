using Crogen.CrogenDialogue.Character;
using Crogen.CrogenDialogue.Editor.UTIL;
using Crogen.CrogenDialogue.Editor.UTIL.CharacterPreviewWindows;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.CharacterPreviewWindows
{
	public class CharacterPreviewWindow : SOEditorWindow<CharacterSO>
	{
		[SerializeField]
		private VisualTreeAsset m_VisualTreeAsset = default;

		public VisualElement BodyPanel {get; private set;}
		public VisualElement ClothsePanel {get; private set;}
		public VisualElement FacePanel { get; private set; }
		public ScrollView BodyScrollView { get; private set; }
		public ScrollView ClothseScrollView { get; private set; }
		public ScrollView FaceScrollView { get; private set; }
		public CharacterPreview CharacterPreview { get; private set; }

		public override CharacterSO SelectedBaseSO => CharacterPreviewSelection.SelectedSO;
		public CharacterBodySO SelectedBodySO { get => CharacterPreviewSelection.SelectedBodySO; set => CharacterPreviewSelection.SelectedBodySO = value; }
		public CharacterElementInfo SelectedClothseInfo { get => CharacterPreviewSelection.SelectedClothseInfo; set => CharacterPreviewSelection.SelectedClothseInfo = value; }
		public CharacterElementInfo SelectedFaceInfo { get => CharacterPreviewSelection.SelectedFaceInfo; set => CharacterPreviewSelection.SelectedFaceInfo = value; }

		public Dictionary<CharacterElementInfo, CharacterElementView> CharacterElementViews { get; private set; } = new();

		[MenuItem("Crogen/CharacterPreviewWindow")]
		public static void ShowExample()
		{
			CharacterPreviewWindow wnd = GetWindow<CharacterPreviewWindow>();
			wnd.titleContent = new GUIContent("CharacterPreviewWindow");
		}

		public override void RebuildGUI(CharacterSO characterSO)
		{
			base.RebuildGUI(characterSO);
			if (SelectedBaseSO == null) return;

			SelectedBaseSO.OnValueChangedEvent = HandleValueChanged;

			var windowVisual = m_VisualTreeAsset.Instantiate();
			windowVisual.style.flexGrow = 1f;
			rootVisualElement.Add(windowVisual);

			// 루트 요소들 찾기
			FindBasePanels(windowVisual);

			// 데이터 로드
			LoadBodyElements();

			CharacterPreviewSelection.OnSelectBodyEvent += x => {
				LoadCharacterElementInfo();
				CharacterPreview.SetBodyVisual(x.sprite);
				SelectedClothseInfo = null;
				SelectedFaceInfo = null;
			};
			CharacterPreviewSelection.OnSelectClothseEvent += x => {
				CharacterPreview.SetClothseVisual(x?.sprite);
			};
			CharacterPreviewSelection.OnSelectFaceEvent += x => {
				CharacterPreview.SetFaceVisual(x?.sprite);
			};

			// 이벤트
			BindNewButtons();
			BindRemoveButtons();
			HandleValueChanged();
		}

		private void HandleValueChanged()
		{
			if (SelectedBaseSO.BodyList.Count == 0)
			{
				BodyPanel.AddToClassList("no-data");
				ClothsePanel.AddToClassList("no-data");
				FacePanel.AddToClassList("no-data");
			}
			else
			{
				BodyPanel.RemoveFromClassList("no-data");
				ClothsePanel.RemoveFromClassList("no-data");
				FacePanel.RemoveFromClassList("no-data");
			}
		}

		private void FindBasePanels(VisualElement windowVisual)
		{
			CharacterPreview = windowVisual.Q<CharacterPreview>();

			BodyPanel = windowVisual.Q<VisualElement>(name: "BodyPanel");
			ClothsePanel = windowVisual.Q<VisualElement>(name: "ClothsePanel");
			FacePanel = windowVisual.Q<VisualElement>(name: "FacePanel");

			BodyScrollView = windowVisual.Q<ScrollView>(name: "BodyScrollView");
			ClothseScrollView = windowVisual.Q<ScrollView>(name: "ClothseScrollView");
			FaceScrollView = windowVisual.Q<ScrollView>(name: "FaceScrollView");

		}

		private void BindNewButtons()
		{
			rootVisualElement.Q<Button>("NewBodyButton").clicked += () => {
				var newBody = SelectedBaseSO.AddNewBody();
				CharacterElementCreator.DrawBodyElementView(newBody.BodyInfo, this);
				SelectedBodySO = newBody;
			};

			rootVisualElement.Q<Button>("NewClothseButton").clicked += () => {
				var newClothse = CharacterPreviewSelection.SelectedBodySO.AddNewClothse();
				CharacterElementCreator.DrawClothseElementView(newClothse, this);
				SelectedClothseInfo = newClothse;
			};

			rootVisualElement.Q<Button>("NewFaceButton").clicked += () => {
				var newFace = CharacterPreviewSelection.SelectedBodySO.AddNewFace();
				CharacterElementCreator.DrawFaceElementView(newFace, this);
				SelectedFaceInfo = newFace;
			};
		}

		private void BindRemoveButtons()
		{
			rootVisualElement.Q<Button>("RemoveBodyButton").clicked += () => {
				if (SelectedBodySO == null) return;
				BodyScrollView.contentContainer.Remove(CharacterElementViews[SelectedBodySO.BodyInfo]);
				CharacterElementViews.Remove(SelectedBodySO.BodyInfo);
				SelectedBaseSO.RemoveBody(SelectedBodySO.BodyInfo);
			};

			rootVisualElement.Q<Button>("RemoveClothseButton").clicked += () => {
				if (SelectedBodySO == null || SelectedClothseInfo == null) return;
				ClothseScrollView.contentContainer.Remove(CharacterElementViews[SelectedClothseInfo]);
				CharacterElementViews.Remove(SelectedClothseInfo);
				SelectedBodySO.RemoveClothse(SelectedClothseInfo);
			};

			rootVisualElement.Q<Button>("RemoveFaceButton").clicked += () => {
				if (SelectedBodySO == null || SelectedFaceInfo == null) return;
				FaceScrollView.contentContainer.Remove(CharacterElementViews[SelectedFaceInfo]);
				CharacterElementViews.Remove(SelectedFaceInfo);
				SelectedBodySO.RemoveClothse(SelectedFaceInfo);
			};
		}

		private void LoadBodyElements(bool loadCharacterElements = true)
		{
			CharacterElementViews.Clear();
			BodyScrollView.contentContainer.Clear();

			for (int i = 0; i < SelectedBaseSO.BodyList.Count; i++)
			{
				CharacterElementCreator.DrawBodyElementView(SelectedBaseSO.BodyList[i].BodyInfo, this);
			}

			if (loadCharacterElements == true)
				LoadCharacterElementInfo();
		}

		public void LoadCharacterElementInfo()
		{
			if (CharacterPreviewSelection.SelectedBodySO == null) return;

			ClothseScrollView.contentContainer.Clear();
			FaceScrollView.contentContainer.Clear();

			for (int i = 0; i < CharacterPreviewSelection.SelectedBodySO.ClothseList.Count; i++)
			{
				CharacterElementCreator.DrawClothseElementView(SelectedBodySO.ClothseList[i], this);
			}

			for (int i = 0; i < CharacterPreviewSelection.SelectedBodySO.FaceList.Count; i++)
			{
				CharacterElementCreator.DrawFaceElementView(SelectedBodySO.FaceList[i], this);
			}
		}
	}
}