using UnityEditor.Experimental.GraphView;

namespace Crogen.CrogenDialogue.Editor.NodeView
{
    public interface IOutputPortsNodeView
    {
		public Port[] Outputs { get; set; }

		public bool RefreshPorts();
		public void RefreshExpandedState();
	}
}
