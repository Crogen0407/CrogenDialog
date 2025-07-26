using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
	// TODO : 나중에 에디터에서 만드는 걸로 바꾸기
	public abstract class NodeSO : ScriptableObject
	{
		[field: SerializeField, HideInEditorWindow] public Vector2 Position { get; set; }
		[field: SerializeField, HideInEditorWindow] public List<NodeSO> InputList { get; private set; } = new();
		[field: SerializeField, HideInEditorWindow] public List<NodeSO> OutputList { get; private set; } = new();

		public abstract string GetNodeName();

		public void AddInput(NodeSO node)
		{
			InputList.Add(node);
		}

		public void RemoveInput(NodeSO node)
		{
			InputList.Remove(node);
		}

		public void AddOutput(NodeSO node)
		{
			OutputList.Add(node);
		}

		public void RemoveOuput(NodeSO node)
		{
			OutputList.Remove(node);
		}
	}
}