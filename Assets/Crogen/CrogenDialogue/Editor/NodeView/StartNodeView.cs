using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.NodeView
{
    public class StartNodeView : Node, IOutputPortsNodeView, IUndeletableNodeView
	{
		public StorytellerBaseSO StorytellerSO { get; private set; }
		private CrogenDialogueGraphView _graphView;
		public override string title { get; set; }

		public Port[] Outputs { get; set; }

		public StartNodeView Initialize(StorytellerBaseSO storytellerSO, CrogenDialogue.Editor.CrogenDialogueGraphView graphView, bool showInputPort = true, bool showOutputPort = true)
		{
			this.title = "Start";
			this.StorytellerSO = storytellerSO;
			this._graphView = graphView;

			// 메인 컨테이너
			var container = new VisualElement();
			container.style.paddingLeft = 8;
			container.style.paddingRight = 8;

			Label titleLebel = new Label(this.title);
			titleLebel.style.paddingLeft = 8;
			titleLebel.style.paddingRight = 8;

			this.titleContainer.Add(titleLebel);
			this.mainContainer.Add(container);

			Outputs = new Port[1];

			CreatePort();

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
			Outputs[0] = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(PortTypes.FlowPort));

			Outputs[0].name = $"StartNode_Output";
			Outputs[0].portName = string.Empty;

			outputContainer.Add(Outputs[0]);
		}

		public override bool IsMovable() => false;
		public override bool IsResizable() => false;
		public override bool IsGroupable() => false;
	}
}
