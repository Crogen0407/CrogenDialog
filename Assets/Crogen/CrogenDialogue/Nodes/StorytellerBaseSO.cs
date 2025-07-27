using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[CreateAssetMenu(fileName = nameof(StorytellerBaseSO), menuName = "CrogenDialogue/StorytellerBaseSO")]
	public class StorytellerBaseSO : ScriptableObject
	{
		[field:SerializeField, HideInEditorWindow] public List<GeneralNodeSO> NodeList { get; private set; } = new List<GeneralNodeSO>();

#if UNITY_EDITOR
		public GeneralNodeSO AddNewNode(System.Type type, Vector2 position)
		{
			var nodeData = ScriptableObject.CreateInstance(type) as GeneralNodeSO;
			nodeData.name = UnityEditor.GUID.Generate().ToString();
			nodeData.Position = position;

			NodeList.Add(nodeData);

			UnityEditor.AssetDatabase.AddObjectToAsset(nodeData, this); // �̷��� story ���ο� ����
			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();

			return nodeData;
		}

		public void RemoveNode(GeneralNodeSO nodeSO)
		{
			if (NodeList.Contains(nodeSO) == false) return;

			UnityEditor.AssetDatabase.RemoveObjectFromAsset(nodeSO);
			DestroyImmediate(nodeSO, true); // ������ �޸𸮿��� ����
			UnityEditor.AssetDatabase.SaveAssets();

			NodeList.Remove(nodeSO);

			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();
		}
#endif
	}
}
