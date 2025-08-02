using Crogen.CrogenDialogue.Editor.NodeView;
using Crogen.CrogenDialogue.Nodes;
using UnityEngine;

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

		public static GeneralNodeView DrawNodeView(NodeSO nodeData, CrogenDialogueGraphView graphView)
		{
			var nodeView = new GeneralNodeView().Initialize(nodeData, DialogueSelection.SelectedStorySO, graphView);

			graphView.AddElement(nodeView);
			nodeView.SetPosition(new Rect(nodeData.Position, Vector2.zero));

			return nodeView;
		}
	}
}