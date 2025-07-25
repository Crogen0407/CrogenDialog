﻿using System;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor
{
    [InitializeOnLoad]
    public static class CrogenDialogueEditorManager
    {
		public static Action<StorytellerSO> OnStorySOSelectedEvent;
        public static StorytellerSO SelectedStorySO { get; set; }

		// Domain reload끄면 안됨!
		static CrogenDialogueEditorManager()
        {

			Selection.selectionChanged += HandleSelectionChanged;
			EditorApplication.quitting += UnsubscribeEvents;
		}

		private static void HandleSelectionChanged()
		{
			SelectedStorySO = Selection.activeObject as StorytellerSO;

			// window가 열려있을 때만 Rebuild하기
			var window = EditorWindow.HasOpenInstances<CrogenDialogueEditorWindow>()
				   ? EditorWindow.GetWindow<CrogenDialogueEditorWindow>()
				   : null; 

			window?.RebuildGUI();
		}

		private static void UnsubscribeEvents()
		{
			Selection.selectionChanged -= HandleSelectionChanged;
			EditorApplication.quitting -= UnsubscribeEvents;
		}

		[UnityEditor.Callbacks.OnOpenAsset]
		public static bool OnOpenAsset(int instanceID, int line)
		{
			var obj = EditorUtility.InstanceIDToObject(instanceID);
			if (obj is StorytellerSO storySO)
			{
				// EditorWindow 열기
				var window = EditorWindow.GetWindow<CrogenDialogueEditorWindow>();
				window.titleContent = new GUIContent("CrogenDialogueWindow");

				CrogenDialogueEditorManager.SelectedStorySO = storySO;

				window.RebuildGUI();

				return true; // 처리했음을 Unity에 알림
			}

			return false; // Unity 기본 동작 유지
		}
	}
}
