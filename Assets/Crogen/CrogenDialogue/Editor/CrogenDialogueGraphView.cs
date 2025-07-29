using Crogen.CrogenDialog.Editor.NodeView;
using Crogen.CrogenDialogue.Editor.NodeView;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor
{
    public class CrogenDialogueGraphView : GraphView
    {
		private CrogenSearchWindow _searchWindow;
		private GraphViewChangedListener _graphViewChangedListener;
		private StorytellerBaseSO SelectedStorySO => DialogueSelection.SelectedStorySO;

		public CrogenDialogueGraphView Initialize(EditorWindow window, StorytellerBaseSO storytellerSO)
		{
			this.StretchToParentSize();

			_graphViewChangedListener = new();
			_searchWindow = ScriptableObject.CreateInstance<CrogenSearchWindow>();
			_searchWindow.Init(this, window);

			nodeCreationRequest = context =>
			{
				SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
			};

			AddManipulators();
			AddGridBackground();
			AddStyles();
			ShowNodeDisplays(storytellerSO);
			ShowPorts();

			graphViewChanged = OnGraphViewChanged;

			return this;
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

		private void ShowNodeDisplays(StorytellerBaseSO storytellerSO)
		{
			NodeViewCreator.CreateStartNodeView(this);

			foreach (var nodeData in storytellerSO.NodeList)
			{
				NodeViewCreator.CreateNodeView(nodeData, this);
			}
		}

		// 삭제할 수 없는 노드 필터링
		public override EventPropagation DeleteSelection()
		{
			// 삭제 허용된 노드만 필터링
			var nodesToDelete = selection
				.Where(elem =>
				{
					if (elem is Node node)
					{
						return !(node is IUndeletableNodeView); // 인터페이스로 삭제 불가 노드 구분
					}
					return true;
				})
				.ToList();

			// 선택 항목에서 허용된 것만 남김
			selection.Clear();
			foreach (var elem in nodesToDelete)
				selection.Add(elem);

			return base.DeleteSelection();
		}

		private GraphViewChange OnGraphViewChanged(GraphViewChange change)
		{
			if (change.movedElements != null)
			{
				foreach (var element in change.movedElements)
				{
					if (element is GeneralNodeView node)
					{
						node.OnMove(); // 삭제 전처리 직접 호출
					}
				}
			}

			if (change.elementsToRemove != null)
			{
				foreach (var element in change.elementsToRemove)
				{
					// 노드가 삭제되었을 때
					if (element is GeneralNodeView node)
						OnNodeRemoved(node);

					// 엣지가 삭제되었을 때
					if (element is Edge edge)
						OnEdgeRemoved(edge);
				}
			}

			if (change.edgesToCreate != null)
			{
				foreach (var edge in change.edgesToCreate)
				{
					var connectedNode = edge.output.node as GeneralNodeView;

					SelectedStorySO.Connections.Add(new NodeConnectionData()
					{
						InputPortName = edge.input.name,
						OutputPortName = edge.output.name,
					});
				}
			}

			return change;
		}

		private void OnNodeRemoved(GeneralNodeView removedNodes)
		{
			removedNodes.OnRemove(); // 삭제 전처리 직접 호출
		}

		private void OnEdgeRemoved(Edge removedEdge)
		{
			GeneralNodeView outputNode = removedEdge.output.node as GeneralNodeView;
			GeneralNodeView inputNode = removedEdge.input.node as GeneralNodeView;

			int removeIndex = DialogueSelection.SelectedStorySO.Connections.FindIndex(x => x.InputPortName.Equals(inputNode.Input.name));
			DialogueSelection.SelectedStorySO.Connections.RemoveAt(removeIndex);
		}

		private void ShowPorts()
		{
			foreach (var port in ports)
			{
				if (port.direction == Direction.Input) continue;

				var outputPort = port;
				var node = (outputPort.node as IOutputPortsNodeView);

				bool isFind = true;

				for (int i = 0; i < SelectedStorySO.Connections.Count; i++)
				{
					var inputPort = ports.FirstOrDefault(x => 
						x.name == SelectedStorySO.Connections[i].InputPortName &&
						port.name == SelectedStorySO.Connections[i].OutputPortName
					);

					if (inputPort == null)
					{
						isFind = false;
						continue;
					}

					var edge = new Edge()
					{
						input = inputPort,
						output = outputPort
					};
					inputPort.Connect(edge);
					outputPort.Connect(edge);
					AddElement(edge);
				}

				// 연결 못했으면 Refresh안해도 됨
				if (isFind == false)
					continue;

				node.RefreshPorts();
				node.RefreshExpandedState();
			}
		}

		public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
		{
			return ports.ToList().Where(
				endPort => endPort.direction != startPort.direction && 
				endPort.node != startPort.node).ToList();
		}
	}
}
