using UnityEditor.Experimental.GraphView;

namespace Crogen.CrogenDialogue.Editor.DialogueWindows.NodeViews
{
	public interface IInputPortNodeView
    {
		public Port Input { get; set; }

		public bool RefreshPorts();
		public void RefreshExpandedState();
	}
}
