using Crogen.CrogenDialogue.Editor.DialogueWindows;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor.UTIL
{
    public abstract class SOEditorWindow<T> : EditorWindow where T : ScriptableObject 
    {
		public abstract T SelectedBaseSO { get; }

		public void CreateGUI()
		{
			RebuildGUI(SelectedBaseSO);
		}

		public virtual void RebuildGUI(T soData)
		{
			rootVisualElement.Clear(); // 이전 내용 제거
		}
	}
}
