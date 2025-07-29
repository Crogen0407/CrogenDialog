using Crogen.CrogenDialog.Editor.NodeView;
using Crogen.CrogenDialogue.Editor.NodeView;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor
{
    public static class NodeViewCreator
    {
        public static GeneralNodeView CreateNodeView(GeneralNodeSO nodeData, CrogenDialogueGraphView graphView)
        {
			var nodeView = new GeneralNodeView().Initialize(nodeData, DialogueSelection.SelectedStorySO, graphView);

			graphView.AddElement(nodeView);
			nodeView.SetPosition(new Rect(nodeData.Position, Vector2.zero));

			return nodeView;
		}

		public static StartNodeView CreateStartNodeView(CrogenDialogueGraphView graphView)
		{
			var nodeView = new StartNodeView().Initialize(DialogueSelection.SelectedStorySO, graphView);

			graphView.AddElement(nodeView);
			nodeView.SetPosition(new Rect(Vector2.zero, Vector2.zero));

			return nodeView;
		}
    }
}
