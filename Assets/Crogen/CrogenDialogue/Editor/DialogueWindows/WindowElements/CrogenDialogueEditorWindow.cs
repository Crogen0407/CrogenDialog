using Crogen.CrogenDialogue.Editor.DialogueWindows.BillboardViews;
using Crogen.CrogenDialogue.Editor.UTIL.DialogueWindows;
using Crogen.CrogenDialogue.Editor.UTIL;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.DialogueWindows.WindowElements
{
	public class CrogenDialogueEditorWindow : SOEditorWindow<StorySO>
	{
		public override StorySO SelectedBaseSO => DialogueSelection.SelectedSO;

		[MenuItem("Crogen/CrogenDialogue")]
		public static void ShowExample()
		{
			CrogenDialogueEditorWindow wnd = GetWindow<CrogenDialogueEditorWindow>();
			wnd.titleContent = new GUIContent("CrogenDialogueWindow");
		}

		public override void RebuildGUI(StorySO storySO)
		{
			base.RebuildGUI(storySO);
			if (SelectedBaseSO == null) return;

			VisualElement root = rootVisualElement;

			rootVisualElement.style.flexDirection = FlexDirection.Row;

			AddViews(root, storySO);
			StyleLoader.AddStyles(root, "CrogenDialogueEditorWindowStyle");
		}

		private void AddViews(VisualElement root, StorySO storytellerBaseSO)
		{
			if (storytellerBaseSO.Billboard != null)
			{
				BillboardView billboardView = new BillboardView().Initialize(storytellerBaseSO);
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