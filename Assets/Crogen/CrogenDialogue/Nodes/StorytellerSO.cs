using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[CreateAssetMenu(fileName = nameof(StorytellerSO), menuName = "CrogenDialogue/StorySO")]
	public class StorytellerSO : ScriptableObject
	{
		[field:SerializeField, HideInEditorWindow] public List<NodeSO> NodeList { get; private set; }

#if UNITY_EDITOR
		public NodeSO AddNewNode(System.Type type, Vector2 position)
		{
			var nodeData = ScriptableObject.CreateInstance(type) as NodeSO;
			nodeData.name = UnityEditor.GUID.Generate().ToString();
			nodeData.Position = position;

			NodeList.Add(nodeData);

			UnityEditor.AssetDatabase.AddObjectToAsset(nodeData, this); // 이러면 story 내부에 묶임
			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();

			return nodeData;
		}

		public void RemoveNode(NodeSO nodeSO)
		{
			if (NodeList.Contains(nodeSO) == false) return;

			UnityEditor.AssetDatabase.RemoveObjectFromAsset(nodeSO);
			DestroyImmediate(nodeSO, true); // 완전히 메모리에서 제거
			UnityEditor.AssetDatabase.SaveAssets();

			NodeList.Remove(nodeSO);

			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();
		}
#endif
	}
}
