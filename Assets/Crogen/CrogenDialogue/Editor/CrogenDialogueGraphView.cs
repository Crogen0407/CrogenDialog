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

		// ������ �� ���� ��� ���͸�
		public override EventPropagation DeleteSelection()
		{
			// ���� ���� ��常 ���͸�
			var nodesToDelete = selection
				.Where(elem =>
				{
					if (elem is Node node)
					{
						return !(node is IUndeletableNodeView); // �������̽��� ���� �Ұ� ��� ����
					}
					return true;
				})
				.ToList();

			// ���� �׸񿡼� ���� �͸� ����
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
						node.OnMove(); // ���� ��ó�� ���� ȣ��
					}
				}
			}

			if (change.elementsToRemove != null)
			{
				foreach (var element in change.elementsToRemove)
				{
					// ��尡 �����Ǿ��� ��
					if (element is GeneralNodeView node)
						OnNodeRemoved(node);

					// ������ �����Ǿ��� ��
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
						SelectedStorySO.StartNode = (edge.input.node as GeneralNodeView)?.BaseNodeSO;
					}
				}
			}

			return change;
		}

		private void OnNodeRemoved(GeneralNodeView removedNodes)
		{
			removedNodes.OnRemove(); // ���� ��ó�� ���� ȣ��
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
					if (generalNode.BaseNodeSO.NextNodes[i] == inputNode.BaseNodeSO)
					{
						removeIndex = i;
						break;
					}
				}

				outputNode.RemoveAt(removeIndex);
			}
			else if (outputNode is StartNodeView startNode)
			{
				SelectedStorySO.StartNode = null;
			}
		}

		private void ShowEdges()
		{
			foreach (var node in nodes)
			{
				GeneralNodeSO[] targetNodes = null;
				Port[] outputPorts = null;

				if (node is GeneralNodeView generalNode)
				{
					targetNodes = generalNode.BaseNodeSO.NextNodes;
					outputPorts = (GetNodeByGuid(generalNode.BaseNodeSO.name) as GeneralNodeView).Outputs;
				}
				else if (node is StartNodeView startNode)
				{
					targetNodes = new GeneralNodeSO[] { SelectedStorySO.StartNode };
					outputPorts = startNode.Outputs;
				}

				for (int i = 0; i < targetNodes.Length; i++)
				{
					if (targetNodes[i] == null) continue;
					string guid = targetNodes[i].name;
					var inputPort = (GetNodeByGuid(guid) as GeneralNodeView).Input;

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
