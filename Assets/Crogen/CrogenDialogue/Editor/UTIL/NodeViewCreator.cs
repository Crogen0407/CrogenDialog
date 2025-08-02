using Crogen.CrogenDialog.Editor.NodeView;
using Crogen.CrogenDialogue.Editor.NodeView;
using Crogen.CrogenDialogue.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.UTIL
{
    public static class NodeViewCreator
    {
        public static StartNodeView DrawStartNodeView(CrogenDialogueGraphView graphView)
		{
			var nodeView = new StartNodeView().Initialize(DialogueSelection.SelectedStorySO, graphView);

			graphView.AddElement(nodeView);
			nodeView.SetPosition(new Rect(Vector2.zero, Vector2.zero));

			return nodeView;
		}

		public static CrogenDialog.Editor.NodeView.GeneralNodeView DrawNodeView(NodeSO nodeData, CrogenDialogueGraphView graphView)
		{
			var nodeView = new CrogenDialog.Editor.NodeView.GeneralNodeView().Initialize(nodeData, DialogueSelection.SelectedStorySO, graphView);

			graphView.AddElement(nodeView);
			nodeView.SetPosition(new Rect(nodeData.Position, Vector2.zero));

			return nodeView;
		}
	}
}