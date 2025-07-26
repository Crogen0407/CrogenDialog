using Crogen.CrogenDialog.Editor;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor
{
    public class CrogenDialogueGraphView : GraphView
    {
		private CrogenSearchWindow _searchWindow;

		public CrogenDialogueGraphView(EditorWindow window, StorytellerSO storytellerSO)
        {
			this.StretchToParentSize();

			_searchWindow = ScriptableObject.CreateInstance<CrogenSearchWindow>();
			_searchWindow.Init(this, window);

			nodeCreationRequest = context =>
			{
				SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
			};

			AddManipulators();
			AddGridBackground();
			AddStyles();
			ShowNodeDisplay(storytellerSO);

			graphViewChanged = OnGraphViewChanged;
		}

		private void AddManipulators()
		{
			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new ContentZoomer());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());
		}

		private void AddGridBackground()
		{
			GridBackground gridBackground = new GridBackground();

			gridBackground.StretchToParentSize();

			Insert(0, gridBackground);
		}

		private void AddStyles()
		{
			StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets\\Crogen\\CrogenDialogue\\Editor\\Resources\\CrogenDialogueGraphViewStyles.uss");
			if (styleSheet == null)
			{
				Debug.LogError("Fail to get style sheet.");
				return;
			}

			styleSheets.Add(styleSheet);
		}

		private void ShowNodeDisplay(StorytellerSO storytellerSO)
		{
			foreach (var nodeData in storytellerSO.NodeList)
			{
				var nodeView = new CrogenDialogueNode(nodeData, storytellerSO);
				AddElement(nodeView);
				nodeView.SetPosition(new Rect(nodeData.Position, Vector2.zero));
			}
		}

		private GraphViewChange OnGraphViewChanged(GraphViewChange change)
		{
			if (change.movedElements != null)
			{
				foreach (var element in change.movedElements)
				{
					if (element is CrogenDialogueNode node)
					{
						node.OnMove(); // 昏力 傈贸府 流立 龋免
					}
				}
			}

			if (change.elementsToRemove != null)
			{
				foreach (var element in change.elementsToRemove)
				{
					if (element is CrogenDialogueNode node)
					{
						node.OnDelete(); // 昏力 傈贸府 流立 龋免
					}
				}
			}

			return change;
		}
	}
}
