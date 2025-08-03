using Crogen.CrogenDialogue.Editor.UTIL;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor
{
	public class CrogenDialogueEditorWindow : EditorWindow
	{
		[MenuItem("Crogen/CrogenDialogue")]
		public static void ShowExample()
		{
			CrogenDialogueEditorWindow wnd = GetWindow<CrogenDialogueEditorWindow>();
			wnd.titleContent = new GUIContent("CrogenDialogueWindow");
		}

		public void CreateGUI()
		{
			RebuildGUI();
		}

		public void RebuildGUI()
		{
			rootVisualElement.Clear(); // 이전 내용 제거

			if (DialogueSelection.SelectedStorySO == null) return;

			VisualElement root = rootVisualElement;

			rootVisualElement.style.flexDirection = FlexDirection.Row;

			AddViews(root, DialogueSelection.SelectedStorySO);
			StyleLoader.AddStyles(root, "CrogenDialogueEditorWindowStyle");
		}

		private void AddViews(VisualElement root, StorytellerBaseSO storytellerBaseSO)
		{
			if (storytellerBaseSO.Billboard != null)
			{
				CrogenDialogueBillboardView billboardView = new CrogenDialogueBillboardView().Initialize(storytellerBaseSO);
				root.Add(billboardView);
			}

			CrogenDialogueGraphView graphView = new CrogenDialogueGraphView().Initialize(this, storytellerBaseSO);
			var graphViewContainer = new VisualElement();
			graphViewContainer.style.flexGrow = 1;
			graphViewContainer.Add(graphView);
			root.Add(graphViewContainer);
		}
	}
}