using Crogen.CrogenDialogue.Editor.DialogueWindows.WindowElements;
using Crogen.CrogenDialogue.Editor.UTIL;
using Crogen.CrogenDialogue.Nodes;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.DialogueWindows.NodeViews
{
	public class GeneralNodeView : Node, IRemovableNodeView, IMovableNodeView, IInputPortNodeView, IOutputPortsNodeView
	{
		public NodeSO BaseNodeSO { get; private set; }
		public StorySO StorytellerBaseSO { get; private set; }
		private CrogenDialogueGraphView _graphView;
		public VisualElement NodeBlockContainer { get; private set; }

		public Port Input { get; set; }
		public Port[] Outputs { get; set; }

		public GeneralNodeView Initialize(NodeSO baseNodeSO, StorySO storytellerBaseSO, CrogenDialogueGraphView graphView)
		{
			this.BaseNodeSO = baseNodeSO;
			this.title = baseNodeSO.GetNodeName();
			this.StorytellerBaseSO = storytellerBaseSO;
			this._graphView = graphView;
			this.viewDataKey = baseNodeSO.GUID;
			this.name = "generalNodeView"; //UI toolkit 스타일 지정용

			// 에디터용 이벤트 구독
			baseNodeSO.OnValueChangedEvent = HandleValueChanged;

			// 이름 필드
			{
				TextField nameTextField = new TextField();
				nameTextField.name = "nameTextField";
				nameTextField.isDelayed = true;
				nameTextField.value = baseNodeSO.name;
				nameTextField.RegisterValueChangedCallback(evt => {
					baseNodeSO.name = evt.newValue;
					EditorUtility.SetDirty(baseNodeSO);
					AssetDatabase.SaveAssets();
				});
				titleContainer.Insert(1, nameTextField);
			}

			// 필드 컨테이너
			{
				var fieldContainer = new VisualElement();
				fieldContainer.name = "fieldContainer";
				fieldContainer.style.paddingLeft = 8;
				fieldContainer.style.paddingRight = 8;

				FieldDrawer.DrawFieldElements(baseNodeSO, fieldContainer);
				this.mainContainer.Add(fieldContainer);
			}

			// 기타 노드 요소
			{
				UpdatePorts();
				StyleLoader.AddStyles(this, "NodeViewStyles");
			}

			// Tooltip
			{
				CheckNodeError();
				CheckNodeWarning();
				UpdateTooltip();
			}

			return this;
		}

		private void HandleValueChanged()
		{
			CheckNodeError();
			CheckNodeWarning();
			UpdateTooltip();
		}

		private void CheckNodeError()
		{
			if (BaseNodeSO.IsError() == true) AddToClassList("node-error");
			else RemoveFromClassList("node-error");
		}
		private void CheckNodeWarning()
		{
			if (BaseNodeSO.IsWarning() == true) AddToClassList("node-warning");
			else RemoveFromClassList("node-warning");
		}
		private void UpdateTooltip()
		{
			this.tooltip = BaseNodeSO.IsError() ? BaseNodeSO.GetErrorText() : BaseNodeSO.IsWarning() ? BaseNodeSO.GetWarningText() : BaseNodeSO.GetTooltipText();
		}
		
		private void UpdatePorts()
		{
			DrawInputPort();
			DrawOutputPorts();

			RefreshPorts();
			RefreshExpandedState();
		}
		private void DrawInputPort()
		{
			// Input은 하나만!
			Input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));

			Input.name = $"{BaseNodeSO.GUID}_Input";
			Input.portName = string.Empty;

			inputContainer.Add(Input);
		}
		private void DrawOutputPorts()	
		{
			Outputs = new Port[BaseNodeSO.GetOutputPortCount()];

			for (int i = 0; i < BaseNodeSO.GetOutputPortCount(); i++)
			{
				Outputs[i] = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

				Outputs[i].name = $"{BaseNodeSO.GUID}_Output_{i}";
				Outputs[i].portName = BaseNodeSO.GetOutputPortsNames()[i];

				outputContainer.Add(Outputs[i]);
			}
		}

		public void OnRemove()
		{
			var inputEdges = Input.connections.ToList();

			foreach (var edge in inputEdges)
			{
				edge.input?.Disconnect(edge);
				edge.output?.Disconnect(edge);
				_graphView.RemoveElement(edge);
			}

			foreach (var output in Outputs)
			{
				var outputEdges = output.connections.ToList();

				foreach (var edge in outputEdges)
				{
					edge.input?.Disconnect(edge);
					edge.output?.Disconnect(edge);
					_graphView.RemoveElement(edge);
				}
			}

			StorytellerBaseSO.RemoveNode(BaseNodeSO);
		}

		public void OnMove()
		{
			BaseNodeSO.Position = GetPosition().position;

			EditorUtility.SetDirty(StorytellerBaseSO);
		}

		public override bool IsResizable() => false;
	}
}
