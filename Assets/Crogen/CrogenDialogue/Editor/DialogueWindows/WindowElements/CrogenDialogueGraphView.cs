using Crogen.CrogenDialogue.Editor.DialogueWindows.NodeViews;
using Crogen.CrogenDialogue.Editor.DialogueWindows.SearchWindows;
using Crogen.CrogenDialogue.Editor.UTIL.DialogueWindows;
using Crogen.CrogenDialogue.Editor.UTIL;
using Crogen.CrogenDialogue.Nodes;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.DialogueWindows.WindowElements
{
	public class CrogenDialogueGraphView : GraphView
    {
		private NodeSearchWindow _nodeSearchWindow;
		private StorySO _storytellerBaseSO;

		public CrogenDialogueGraphView Initialize(EditorWindow window, StorySO storytellerBaseSO)
		{
			this.StretchToParentSize();

			this._storytellerBaseSO = storytellerBaseSO;

			_nodeSearchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
			_nodeSearchWindow.Init(this, window);

			nodeCreationRequest = context =>
			{
				SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _nodeSearchWindow);
			};

			StyleLoader.AddStyles(this, "CrogenDialogueGraphViewStyles");

			AddManipulators();
			AddGridBackground();
			ShowNodeDisplays(storytellerBaseSO);
			ShowEdges();

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

		private void ShowNodeDisplays(StorySO storytellerSO)
		{
			NodeViewCreator.DrawStartNodeView(this);

			foreach (var nodeData in storytellerSO.NodeList)
			{
				NodeViewCreator.DrawNodeView(nodeData, this);
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
					var connectedNode = edge.output.node;
					int portIndex = edge.output.parent.IndexOf(edge.output);
					if (connectedNode is GeneralNodeView generalNode)
					{
						generalNode.BaseNodeSO.NextNodes[portIndex] = (edge.input.node as GeneralNodeView)?.BaseNodeSO;
					}	
					else if (connectedNode is StartNodeView startNode)
					{
						_storytellerBaseSO.StartNode = (edge.input.node as GeneralNodeView)?.BaseNodeSO;
					}
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
			var outputNode = removedEdge.output.node;

			if (outputNode is GeneralNodeView generalNode)
			{
				GeneralNodeView inputNode = removedEdge.input.node as GeneralNodeView;

				int removeIndex = 0;

				for (int i = 0; i < generalNode.BaseNodeSO.NextNodes.Length; i++)
				{
					if (generalNode.BaseNodeSO.NextNodes[i] != null && generalNode.BaseNodeSO.NextNodes[i].GUID.Equals(inputNode.BaseNodeSO.GUID))
					{
						removeIndex = i;
						break;
					}
				}

				generalNode.BaseNodeSO.NextNodes[removeIndex] = null;
			}
			else if (outputNode is StartNodeView startNode)
			{
				_storytellerBaseSO.StartNode = null;
			}
		}

		private void ShowEdges()
		{
			foreach (var node in nodes)
			{
				NodeSO[] nextNodes = null;
				Port[] outputPorts = null;

				if (node is GeneralNodeView generalNode)
				{
					nextNodes = generalNode.BaseNodeSO.NextNodes;
					outputPorts = (GetNodeByGuid(generalNode.BaseNodeSO.GUID) as GeneralNodeView).Outputs;
				}
				else if (node is StartNodeView startNode)
				{
					nextNodes = new NodeSO[] { _storytellerBaseSO.StartNode };
					outputPorts = startNode.Outputs;
				}

				for (int i = 0; i < nextNodes.Length; i++)
				{
					if (nextNodes[i] == null) continue;
					string guid = nextNodes[i].GUID;
					var generalNodeView = (GetNodeByGuid(guid) as GeneralNodeView);
					if (generalNodeView == null) continue;
					var inputPort = generalNodeView.Input;

					var edge = new Edge()
					{
						input = inputPort,
						output = outputPorts[i]
					};
					inputPort.Connect(edge);
					outputPorts[i].Connect(edge);
					AddElement(edge);
				}
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
