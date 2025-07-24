using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialog
{
	// TODO : 나중에 에디터에서 만드는 걸로 바꾸기
	[CreateAssetMenu(fileName = "NodeSO", menuName = "CrogenDialog/NodeSO")]
	public abstract class NodeSO : ScriptableObject
	{
		[field:SerializeField] public List<NodeSO> InputList { get; private set; }
		[field:SerializeField] public List<NodeSO> OutputList { get; private set; }

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