using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor
{

	public class CrogenDialogueEditorWindow : EditorWindow
	{
		[SerializeField]
		private VisualTreeAsset m_VisualTreeAsset = default;

		[MenuItem("Crogen/CrogenDialog")]
		public static void ShowExample()
		{
			CrogenDialogueEditorWindow wnd = GetWindow<CrogenDialogueEditorWindow>();
			wnd.titleContent = new GUIContent("CrogenDialogWindow");
		}

		public void CreateGUI()
		{
			// Each editor window contains a root VisualElement object
			VisualElement root = rootVisualElement;

			// VisualElements objects can contain other VisualElement following a tree hierarchy.
			VisualElement label = new Label("Hello World! From C#");
			root.Add(label);

			// Instantiate UXML
			VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
			root.Add(labelFromUXML);
		}
	}

}