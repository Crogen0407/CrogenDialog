using Crogen.CrogenDialogue.Editor.DialogueWindows.WindowElements;
using System;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor.UTIL.DialogueWindows
{
	[InitializeOnLoad]
	public static class DialogueSelection
    {
		public static Action<StorySO> OnSOSelectedEvent;
		public static StorySO SelectedSO { get; set; }

		// Domain reload끄면 안됨!
		static DialogueSelection()
		{
			Selection.selectionChanged += HandleSelectionChanged;
			EditorApplication.quitting += UnsubscribeEvents;
		}

		private static void UnsubscribeEvents()
		{
			Selection.selectionChanged -= HandleSelectionChanged;
			EditorApplication.quitting -= UnsubscribeEvents;
		}

		private static void HandleSelectionChanged()
		{
			SelectedSO = Selection.activeObject as StorySO;

			// window가 열려있을 때만 Rebuild하기
			var window = EditorWindow.HasOpenInstances<CrogenDialogueEditorWindow>()
				   ? EditorWindow.GetWindow<CrogenDialogueEditorWindow>()
				   : null;

			window?.RebuildGUI(SelectedSO);
		}

		[UnityEditor.Callbacks.OnOpenAsset]
		public static bool OnOpenAsset(int instanceID, int line)
		{
			var obj = EditorUtility.InstanceIDToObject(instanceID);
			if (obj is StorySO storySO)
			{
				// EditorWindow 열기
				var window = EditorWindow.GetWindow<CrogenDialogueEditorWindow>();
				window.titleContent = new GUIContent("CrogenDialogueWindow");

				SelectedSO = storySO;

				window.RebuildGUI(storySO);

				return true; // 처리했음을 Unity에 알림
			}

			return false; // Unity 기본 동작 유지
		}
	}
}
