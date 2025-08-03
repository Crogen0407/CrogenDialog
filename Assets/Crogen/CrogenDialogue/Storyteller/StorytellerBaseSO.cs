using Crogen.CrogenDialogue.Billboard;
using Crogen.CrogenDialogue.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[CreateAssetMenu(fileName = nameof(StorytellerBaseSO), menuName = "CrogenDialogue/StorytellerBaseSO")]
	public class StorytellerBaseSO : ScriptableObject
	{
		[field: SerializeField] public BillboardSO Billboard { get; private set; }
		[field: SerializeField] public NodeSO StartNode { get; set; }
		[field: SerializeField] public List<NodeSO> NodeList { get; private set; } = new List<NodeSO>();

		public bool IsError()
		{
			for (int i = 0; i < NodeList.Count; i++)
			{
				if (NodeList[i].IsError())
					return true;
			}

			return false;
		}

#if UNITY_EDITOR
		public NodeSO AddNewNode(System.Type type, Vector2 position)
		{
			var nodeData = ScriptableObject.CreateInstance(type) as NodeSO;
			nodeData.GUID = UnityEditor.GUID.Generate().ToString();
			nodeData.Position = position;
			nodeData.StorytellerBaseSO = this;

			NodeList.Add(nodeData);

			UnityEditor.AssetDatabase.AddObjectToAsset(nodeData, this); // 이러면 SO 하단에 묶임
			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();

			return nodeData;
		}

		public void RemoveNode(NodeSO nodeSO)
		{
			if (NodeList.Contains(nodeSO) == false) return;

			NodeList.Remove(nodeSO);

			UnityEditor.AssetDatabase.RemoveObjectFromAsset(nodeSO);
			DestroyImmediate(nodeSO, true); // 완전히 메모리에서 제거
			UnityEditor.AssetDatabase.SaveAssets();

			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();
		}
#endif
	}
}
