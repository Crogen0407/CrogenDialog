using Crogen.CrogenDialogue.Editor.DialogueWindows.NodeViews;
using Crogen.CrogenDialogue.Editor.DialogueWindows.WindowElements;
using Crogen.CrogenDialogue.Nodes;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor.UTIL.DialogueWindows
{
	public static class NodeViewCreator
    {
        public static StartNodeView DrawStartNodeView(CrogenDialogueGraphView graphView)
		{
			var nodeView = new StartNodeView().Initialize(DialogueSelection.SelectedSO, graphView);

			graphView.AddElement(nodeView);
			nodeView.SetPosition(new Rect(Vector2.zero, Vector2.zero));

			return nodeView;
		}

		public static GeneralNodeView DrawNodeView(NodeSO nodeData, CrogenDialogueGraphView graphView)
		{
			var nodeView = new GeneralNodeView().Initialize(nodeData, DialogueSelection.SelectedSO, graphView);

			graphView.AddElement(nodeView);
			nodeView.SetPosition(new Rect(nodeData.Position, Vector2.zero));

			return nodeView;
		}
	}
}