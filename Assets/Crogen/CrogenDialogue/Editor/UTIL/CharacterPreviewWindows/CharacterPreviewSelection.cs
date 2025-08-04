using Crogen.CrogenDialogue.Character;
using Crogen.CrogenDialogue.Editor.CharacterPreviewWindows;
using System;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor.UTIL.CharacterPreviewWindows
{
	[InitializeOnLoad]
	public static class CharacterPreviewSelection
    {
		public static CharacterSO SelectedSO { get; set; }

		private static CharacterBodySO _selectedBodySO;
		private static CharacterElementInfo _selectedClothseInfo;
		private static CharacterElementInfo _selectedFaceInfo;

		public static CharacterBodySO SelectedBodySO { get => _selectedBodySO; set { _selectedBodySO = value; OnSelectBodyEvent?.Invoke(value.BodyInfo); }}
		public static CharacterElementInfo SelectedClothseInfo { get => _selectedClothseInfo; set { _selectedClothseInfo = value; OnSelectClothseEvent?.Invoke(value); }}
		public static CharacterElementInfo SelectedFaceInfo { get => _selectedFaceInfo; set { _selectedFaceInfo = value; OnSelectFaceEvent?.Invoke(value); }}

		public static Action<CharacterElementInfo> OnSelectBodyEvent;
		public static Action<CharacterElementInfo> OnSelectClothseEvent;
		public static Action<CharacterElementInfo> OnSelectFaceEvent;

		// Domain reload끄면 안됨!
		static CharacterPreviewSelection()
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
			SelectedSO = Selection.activeObject as CharacterSO;

			// window가 열려있을 때만 Rebuild하기
			var window = EditorWindow.HasOpenInstances<CharacterPreviewWindow>()
				   ? EditorWindow.GetWindow<CharacterPreviewWindow>()
				   : null;

			window?.RebuildGUI(SelectedSO);
		}

		[UnityEditor.Callbacks.OnOpenAsset]
		public static bool OnOpenAsset(int instanceID, int line)
		{
			var obj = EditorUtility.InstanceIDToObject(instanceID);
			if (obj is CharacterSO characterSO)
			{
				// EditorWindow 열기
				var window = EditorWindow.GetWindow<CharacterPreviewWindow>();
				window.titleContent = new GUIContent("CharacterPreviewWindow");

				SelectedSO = characterSO;

				window.RebuildGUI(characterSO);

				return true; // 처리했음을 Unity에 알림
			}

			return false; // Unity 기본 동작 유지
		}
	}
}
