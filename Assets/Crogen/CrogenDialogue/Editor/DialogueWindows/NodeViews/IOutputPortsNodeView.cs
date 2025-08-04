using UnityEditor.Experimental.GraphView;

namespace Crogen.CrogenDialogue.Editor.DialogueWindows.NodeViews
{
	public interface IOutputPortsNodeView
    {
		public Port[] Outputs { get; set; }

		public bool RefreshPorts();
		public void RefreshExpandedState();
	}
}
