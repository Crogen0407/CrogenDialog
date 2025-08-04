using System;
using UnityEngine;

namespace Crogen.CrogenDialogue.Nodes
{
	public abstract class NodeSO : ScriptableObject
	{
		[field: SerializeField, HideInEditorWindow] public string GUID { get; set; }
		[field: SerializeField, HideInEditorWindow] public StorySO StorySO { get; set; }
		[field: SerializeField, HideInEditorWindow] public NodeSO[] NextNodes;
		[field: SerializeField, HideInEditorWindow] public Vector2 Position { get; set; }

		public virtual string[] GetOutputPortsNames() => new[] { string.Empty };
		public virtual int GetOutputPortCount() => 1;
		public abstract string GetNodeName();
		public abstract string GetTooltipText();

		public virtual string GetErrorText() => "에러입니다.";
		public virtual string GetWarningText() => "워닝입니다.";
		public virtual bool IsError() => false;
		public virtual bool IsWarning() => false;

#if UNITY_EDITOR
		public Action OnValueChangedEvent;
#endif

		protected virtual void OnValidate()
		{
#if UNITY_EDITOR
			OnValueChangedEvent?.Invoke();
#endif
		}

		protected virtual void OnEnable()
		{
			if (NextNodes == null) 
				NextNodes = new NodeSO[GetOutputPortCount()];
		}

		public virtual void Go(Storyteller storyteller)
		{
			NextNodes[0]?.Go(storyteller);
		}
	}
}