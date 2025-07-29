using System;
using UnityEditor;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor
{
    [InitializeOnLoad]
    public static class CrogenDialogueEditorManager
    {
		// Domain reload끄면 안됨!
		static CrogenDialogueEditorManager()
        {
			EditorApplication.quitting += UnsubscribeEvents;
		}

		private static void UnsubscribeEvents()
		{
			EditorApplication.quitting -= UnsubscribeEvents;
		}
	}
}
