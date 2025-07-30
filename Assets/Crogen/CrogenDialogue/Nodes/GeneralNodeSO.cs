using Crogen.CrogenDialogue.NodeBlocks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	[RegisterNode] // 기본 노드라는 뜻. 사용자는 이거 붙힐 필요없음
	public class GeneralNodeSO : ScriptableObject
	{
		[field: SerializeField, HideInEditorWindow] public string GUID { get; set; }
		[field: SerializeField, HideInEditorWindow] public GeneralNodeSO[] NextNodes;
		[field: SerializeField, HideInEditorWindow] public List<NodeBlockSO> NodeBlockList { get; private set; } = new();
		[field: SerializeField, HideInEditorWindow] public Vector2 Position { get; set; }

		public virtual string[] GetOutputPortsNames() => new[] { string.Empty };
		public virtual string GetNodeName() => "General";
		public virtual int GetOutputPortCount()=> 1;
		public virtual string GetTooltip() => "기본적인 노드입니다.";

#if UNITY_EDITOR
		public Action OnValueChangedEvent;

		protected virtual void OnValidate()
		{
			OnValueChangedEvent?.Invoke();
		}
#endif
		protected virtual void OnEnable()
		{
			if (NextNodes == null) 
				NextNodes = new GeneralNodeSO[GetOutputPortCount()];
		}

		public virtual void Go(Storyteller storyteller)
		{
			for (int i = 0; i < NodeBlockList.Count; i++)
				NodeBlockList[i]?.Go(this);

			for (int i = 0; i < NextNodes.Length; i++)
				NextNodes[i]?.Go(storyteller);
		}
	}
}