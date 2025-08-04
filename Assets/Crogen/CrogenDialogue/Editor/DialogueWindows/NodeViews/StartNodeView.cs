using Crogen.CrogenDialogue.Editor.DialogueWindows.WindowElements;
using Crogen.CrogenDialogue.Editor.UTIL;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.DialogueWindows.NodeViews
{
	public class StartNodeView : Node, IOutputPortsNodeView, IUndeletableNodeView
	{
		public StorySO StorytellerSO { get; private set; }
		private CrogenDialogueGraphView _graphView;

		public Port[] Outputs { get; set; }

		public StartNodeView Initialize(StorySO storytellerSO, CrogenDialogueGraphView graphView, bool showInputPort = true, bool showOutputPort = true)
		{
			this.title = "Start";
			this.StorytellerSO = storytellerSO;
			this._graphView = graphView;
			name = "generalNodeView";

			// 메인 컨테이너
			var container = new VisualElement();
			container.style.paddingLeft = 8;
			container.style.paddingRight = 8;

			this.mainContainer.Add(container);

			Outputs = new Port[1];

			CreatePort();

			StyleLoader.AddStyles(this, "NodeViewStyles");

			return this;
		}

		private void CreatePort()
		{
			CreateOutputPorts();

			RefreshPorts();
			RefreshExpandedState();
		}
		private void CreateOutputPorts()
		{
			Outputs[0] = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

			Outputs[0].name = $"StartNode_Output";
			Outputs[0].portName = string.Empty;

			outputContainer.Add(Outputs[0]);
		}

		public override bool IsMovable() => false;
		public override bool IsResizable() => false;
		public override bool IsGroupable() => false;
	}
}
