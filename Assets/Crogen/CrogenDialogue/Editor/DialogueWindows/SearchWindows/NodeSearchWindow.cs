using Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes;
using Crogen.CrogenDialogue.Editor.DialogueWindows.WindowElements;
using Crogen.CrogenDialogue.Editor.UTIL;
using Crogen.CrogenDialogue.Editor.UTIL.DialogueWindows;
using Crogen.CrogenDialogue.Nodes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.DialogueWindows.SearchWindows
{
	public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
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
			var subclasses = SubclassLoader.Get<NodeSO>();
			var searchTreeEntryList = new List<SearchTreeEntry>
			{
				new SearchTreeGroupEntry(new GUIContent("Create Node"), 0)
			};

			// 중복 그룹 방지용 캐시
			HashSet<string> createdGroups = new HashSet<string>();

			foreach (var type in subclasses)
			{
				var attr = type.GetCustomAttribute<NodePathAttribute>();

				if (attr != null && !string.IsNullOrWhiteSpace(attr.path))
				{
					string[] splitPath = attr.path.Split('/');
					int level = 1;
					string currentPath = "";

					// 그룹 경로 따라 생성
					foreach (var group in splitPath)
					{
						currentPath += "/" + group;
						if (!createdGroups.Contains(currentPath))
						{
							searchTreeEntryList.Add(new SearchTreeGroupEntry(new GUIContent(group), level));
							createdGroups.Add(currentPath);
						}
						level++;
					}

					searchTreeEntryList.Add(new SearchTreeEntry(new GUIContent(type.Name.Replace("SO", string.Empty), _icon))
					{
						level = splitPath.Length + 1,
						userData = type
					});
				}
				else
				{
					// 경로 없는 노드는 바로 1레벨에
					searchTreeEntryList.Add(new SearchTreeEntry(new GUIContent(type.Name.Replace("SO", string.Empty), _icon))
					{
						level = 1,
						userData = type
					});
				}
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

			if (entry.userData is System.Type type && (type.IsSubclassOf(typeof(NodeSO)) || type == typeof(NodeSO)))
			{
				var nodeData = DialogueSelection.SelectedSO.AddNewNode(type, graphMousePos);

				NodeViewCreator.DrawNodeView(nodeData, _graphView);

				return true;
			}

			return false;
		}
	}
}
