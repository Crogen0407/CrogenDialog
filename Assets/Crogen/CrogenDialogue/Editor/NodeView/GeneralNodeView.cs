using Crogen.CrogenDialogue;
using Crogen.CrogenDialogue.Editor;
using Crogen.CrogenDialogue.Editor.NodeView;
using Crogen.CrogenDialogue.Nodes;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialog.Editor.NodeView
{
	public class GeneralNodeView : Node, IRemovableNodeView, IMovableNodeView, IInputPortNodeView, IOutputPortsNodeView
	{
		public GeneralNodeSO BaseNodeSO { get; private set; }
		public StorytellerBaseSO StorytellerSO { get; private set; }
		private CrogenDialogueGraphView _graphView;
		public override string title { get; set; }

		public Port Input { get; set; }
		public Port[] Outputs { get; set; }

		public GeneralNodeView Initialize(GeneralNodeSO baseNodeSO, StorytellerBaseSO storytellerSO, CrogenDialogue.Editor.CrogenDialogueGraphView graphView, bool showInputPort = true, bool showOutputPort = true)
		{
			this.BaseNodeSO = baseNodeSO;
			this.tooltip = BaseNodeSO.GetTooltip();
			this.title = baseNodeSO.GetNodeName();
			this.StorytellerSO = storytellerSO;
			this._graphView = graphView;

			baseNodeSO.OnValueChangedEvent = HandleValueChanged;

			// 검색용
			viewDataKey = baseNodeSO.name;

			// 메인 컨테이너
			var container = new VisualElement();
			container.style.paddingLeft = 8;
			container.style.paddingRight = 8;

			CreateFieldElements(baseNodeSO, container);

			Label titleLebel = new Label(this.title);
			titleLebel.style.paddingLeft = 8;
			titleLebel.style.paddingRight = 8;

			this.titleContainer.Add(titleLebel);
			this.mainContainer.Add(container);

			Outputs = new Port[baseNodeSO.GetOutputPortCount()];

			CreatePorts();

			return this;
		}

		private void HandleValueChanged()
		{
			this.tooltip = BaseNodeSO.GetTooltip();
		}

		private void CreateFieldElements(GeneralNodeSO baseNodeSO, VisualElement container)
		{
			// SerializedObject 생성
			SerializedObject soSerialized = new SerializedObject(baseNodeSO);
			var iterator = soSerialized.GetIterator();

			if (iterator.NextVisible(true))
			{
				do
				{
					if (IsCanRender(iterator.name, baseNodeSO) == false) continue;

					PropertyField propField = new PropertyField(iterator.Copy());
					propField.Bind(soSerialized);
					container.Add(propField);
				}
				while (iterator.NextVisible(false));
			}
		}

		private bool IsCanRender(string propertyName, GeneralNodeSO baseNodeSO)
		{
			Type nodeType = baseNodeSO.GetType();

			if (nodeType.IsDefined(typeof(RegisterNodeAttribute), false)
				&& propertyName == "m_Script") return false; // 에디터 자체 정의 노드는 렌더링하지 않음

			FieldInfo fieldInfo = GetAnyField(nodeType, propertyName);

			if (fieldInfo != null)
			{
				if (fieldInfo.IsDefined(typeof(HideInEditorWindowAttribute), false))
				{
					return false;
				}
			}
			
			return true;
		}

		private	FieldInfo GetAnyField(Type type, string name)
		{
			while (type != null)
			{
				var field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
				if (field != null)
					return field;
				type = type.BaseType;
			}
			return null;
		}

		private void CreatePorts()
		{
			CreateInputPort();
			CreateOutputPorts();

			RefreshPorts();
			RefreshExpandedState();
		}

		private void CreateInputPort()
		{
			// Input은 하나만!
			Input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(PortTypes.FlowPort));

			Input.name = $"{BaseNodeSO.name}_Input";
			Input.portName = string.Empty;

			inputContainer.Add(Input);
		}

		private void CreateOutputPorts()	
		{
			for (int i = 0; i < BaseNodeSO.GetOutputPortCount(); i++)
			{
				Outputs[i] = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(PortTypes.FlowPort));

				Outputs[i].name = $"{BaseNodeSO.name}_Output_{i}";
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

			StorytellerSO.RemoveNode(BaseNodeSO);
		}

		public void OnMove()
		{
			BaseNodeSO.Position = GetPosition().position;

			EditorUtility.SetDirty(StorytellerSO);
		}

		public override bool IsResizable() => false;
	}
}
