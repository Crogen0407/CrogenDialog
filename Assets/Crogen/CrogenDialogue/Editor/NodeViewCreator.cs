using Crogen.CrogenDialog.Editor;
using UnityEngine;

namespace Crogen.CrogenDialogue.Editor
{
    public static class NodeViewCreator
    {
        public static CrogenDialogueNodeView CreateNodeView(GeneralNodeSO nodeData, CrogenDialogueGraphView graphView)
        {
			var nodeView = new CrogenDialogueNodeView().Initialize(nodeData, CrogenDialogueEditorManager.SelectedStorySO, graphView);

			graphView.AddElement(nodeView);
			nodeView.SetPosition(new Rect(nodeData.Position, Vector2.zero));

			return nodeView;
		}
    }
}
