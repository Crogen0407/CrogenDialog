using Crogen.CrogenDialogue;
using Crogen.CrogenDialogue.Editor;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Rendering.CameraUI;

namespace Crogen.CrogenDialog.Editor
{
	public class CrogenDialogueNode : Node
	{
		internal GeneralNodeSO BaseNodeSO { get; private set; }
		internal StorytellerBaseSO StorytellerSO { get; private set; }
		private CrogenDialogueGraphView _graphView;
		public override string title => BaseNodeSO?.GetNodeName();

		public Port Input { get; private set; }
		private Port[] _outputs;

		public CrogenDialogueNode(GeneralNodeSO baseNodeSO, StorytellerBaseSO storytellerSO, CrogenDialogue.Editor.CrogenDialogueGraphView graphView)
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

			_outputs = new Port[baseNodeSO.GetOutputPortCount()];

			CreatePorts();
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
				_outputs[i] = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(PortTypes.FlowPort));

				_outputs[i].name = $"{BaseNodeSO.name}_Output_{i}";
				_outputs[i].portName = BaseNodeSO.GetOutputPortsNames()[i];

				outputContainer.Add(_outputs[i]);
			}
		}

		internal void OnDelete()
		{
			StorytellerSO.RemoveNode(BaseNodeSO);
		}

		internal void OnMove()
		{
			BaseNodeSO.Position = GetPosition().position;

			EditorUtility.SetDirty(StorytellerSO);
		}
	}
}
