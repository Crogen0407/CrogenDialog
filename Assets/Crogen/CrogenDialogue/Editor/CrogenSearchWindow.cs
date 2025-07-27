using Crogen.CrogenDialog.Editor;
using Crogen.CrogenDialogue.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue
{
	public class CrogenSearchWindow : ScriptableObject, ISearchWindowProvider
	{
		private CrogenDialogueGraphView _graphView;
		private EditorWindow _editorWindow;
		private Texture2D _icon;

		public void Init(CrogenDialogueGraphView graphView, EditorWindow editorWindow)
		{
			_graphView = graphView;
			_editorWindow = editorWindow;

			// 빈 텍스처 (아이콘 안 깨지게)
			_icon = new Texture2D(1, 1);
			_icon.SetPixel(0, 0, Color.clear);
			_icon.Apply();
		}

		public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
		{
			var subclasses = NodeList.Get();
			var searchTreeEntryList = new List<SearchTreeEntry>
			{
				new SearchTreeGroupEntry(new GUIContent("Create Node")),
			};

			for (int i = 0; i < subclasses.Count; i++)
			{
				searchTreeEntryList.Add(new SearchTreeEntry(new GUIContent(subclasses[i].Name.Replace("SO", string.Empty), _icon))
				{
					level = 1,
					userData = subclasses[i]
				});
			}

			return searchTreeEntryList;
		}

		public bool OnSelectEntry(SearchTreeEntry entry, SearchWindowContext context)
		{
			Vector2 mousePosition = _editorWindow.rootVisualElement.ChangeCoordinatesTo(
				_editorWindow.rootVisualElement.parent,
				context.screenMousePosition - _editorWindow.position.position
			);

			Vector2 graphMousePos = _graphView.contentViewContainer.WorldToLocal(mousePosition);

			if (entry.userData is System.Type type && (type.IsSubclassOf(typeof(GeneralNodeSO)) || type == typeof(GeneralNodeSO)))
			{
				var nodeData = CrogenDialogueEditorManager.SelectedStorySO.AddNewNode(type, graphMousePos);

				var nodeView = new CrogenDialogueNode(nodeData, CrogenDialogueEditorManager.SelectedStorySO, _graphView);
				nodeView.SetPosition(new Rect(graphMousePos, Vector2.zero));
				_graphView.AddElement(nodeView);

				return true;
			}

			return false;
		}
	}
}
