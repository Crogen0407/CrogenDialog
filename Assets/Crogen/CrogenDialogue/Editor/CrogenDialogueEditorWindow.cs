using Crogen.CrogenDialog.Editor;
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

			if (CrogenDialogueEditorManager.SelectedStorySO == null) return;

			VisualElement root = rootVisualElement;

			AddGraphView(root, CrogenDialogueEditorManager.SelectedStorySO);
			AddStyleSheet(root);
		}

		private CrogenDialogueGraphView AddGraphView(VisualElement root, StorytellerBaseSO storytellerSO)
		{
			CrogenDialogueGraphView graphView = new CrogenDialogueGraphView().Initialize(this, storytellerSO);
			root.Add(graphView);

			return graphView;
		}

		private StyleSheet AddStyleSheet(VisualElement root)
		{
			StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets\\Crogen\\CrogenDialogue\\Editor\\Resources\\CrogenDialogueVariables.uss");
			if (styleSheet == null)
			{
				Debug.LogError("Fail to get style sheet.");
				return null;
			}

			root.styleSheets.Add(styleSheet);

			return styleSheet;
		}
	}
}