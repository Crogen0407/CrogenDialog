using Crogen.CrogenDialogue.Editor.Resources;
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
			rootVisualElement.Clear(); // ���� ���� ����

			if (DialogueSelection.SelectedStorySO == null) return;

			VisualElement root = rootVisualElement;

			rootVisualElement.style.flexDirection = FlexDirection.Row;

			AddViews(root, DialogueSelection.SelectedStorySO);
			StyleLoader.AddStyles(root, "CrogenDialogueEditorWindowStyle");
		}

		private void AddViews(VisualElement root, StorytellerBaseSO storytellerBaseSO)
		{
			CrogenDialogueGraphView graphView = new CrogenDialogueGraphView().Initialize(this, storytellerBaseSO);
			root.Add(graphView);

			CrogenDialogueInspectorView inspectorView = new CrogenDialogueInspectorView().Initialize(storytellerBaseSO);
			root.Add(inspectorView);

		}
	}
}