using Crogen.CrogenDialogue;
using Crogen.CrogenDialogue.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialog.Editor
{
	public class CrogenDialogueNodeView : Node
	{
		internal GeneralNodeSO BaseNodeSO { get; private set; }
		internal StorytellerBaseSO StorytellerSO { get; private set; }
		private CrogenDialogueGraphView _graphView;
		public override string title => BaseNodeSO?.GetNodeName();

		public Port Input { get; private set; }
		public Port[] Outputs { get; private set; }

		public CrogenDialogueNodeView Initialize(GeneralNodeSO baseNodeSO, StorytellerBaseSO storytellerSO, CrogenDialogue.Editor.CrogenDialogueGraphView graphView)
		{
			this.BaseNodeSO = baseNodeSO;
			this.StorytellerSO = storytellerSO;
			this._graphView = graphView;

			// 메인 컨테이너
			var container = new VisualElement();
			container.style.paddingLeft = 8;
			container.style.paddingRight = 8;

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

			Label title = new Label("This is title");
			title.style.paddingLeft = 8;
			title.style.paddingRight = 8;

			this.titleContainer.Add(title);
			this.mainContainer.Add(container);

			Outputs = new Port[baseNodeSO.GetOutputPortCount()];

			CreatePorts();

			return this;
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
			CreateOutputPort();

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

		private void CreateOutputPort()
		{
			for (int i = 0; i < BaseNodeSO.GetOutputPortCount(); i++)
			{
				Outputs[i] = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(PortTypes.FlowPort));

				Outputs[i].name = $"{BaseNodeSO.name}_Output_{i}";
				Outputs[i].portName = BaseNodeSO.GetOutputPortsNames()[i];

				outputContainer.Add(Outputs[i]);
			}
		}

		internal void OnDelete()
		{
			var inputEdges = Input.connections.ToList(); // ToList()로 먼저 복사!

			foreach (var edge in inputEdges)
			{
				edge.input?.Disconnect(edge);
				edge.output?.Disconnect(edge);
				_graphView.RemoveElement(edge);
			}


			foreach (var output in Outputs)
			{
				var outputEdges = output.connections.ToList(); // 마찬가지로 복사

				foreach (var edge in outputEdges)
				{
					edge.input?.Disconnect(edge);
					edge.output?.Disconnect(edge);
					_graphView.RemoveElement(edge);
				}
			}

			StorytellerSO.RemoveNode(BaseNodeSO);
		}

		internal void OnMove()
		{
			BaseNodeSO.Position = GetPosition().position;

			EditorUtility.SetDirty(StorytellerSO);
		}
	}
}
