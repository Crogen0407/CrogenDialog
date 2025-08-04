using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue.Character
{
    [CreateAssetMenu(fileName = nameof(CharacterSO), menuName = "CrogenDialogue/CharacterSO")]
    public class CharacterSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public List<CharacterBodySO> BodyList { get; private set; }

#if UNITY_EDITOR
        public Action OnValueChangedEvent;
#endif

		protected virtual void OnValidate()
		{
#if UNITY_EDITOR
			OnValueChangedEvent?.Invoke();
#endif
		}

		public CharacterBodySO FindBody(CharacterElementInfo bodyInfo)
			=> BodyList.Find(x => x.BodyInfo.Equals(bodyInfo));

		public CharacterBodySO FindBody(string bodyName)
			=> BodyList.Find(x => x.BodyInfo.name.Equals(bodyName));

#if UNITY_EDITOR
		public CharacterBodySO AddNewBody()
		{
			var bodyData = ScriptableObject.CreateInstance(typeof(CharacterBodySO)) as CharacterBodySO;

			BodyList.Add(bodyData);

			UnityEditor.AssetDatabase.AddObjectToAsset(bodyData, this); // 이러면 SO 하단에 묶임
			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();
			return bodyData;
		}

		public void RemoveBody(CharacterBodySO characterBodySO)
		{
			if (BodyList.Remove(characterBodySO) == false) return;

			UnityEditor.AssetDatabase.RemoveObjectFromAsset(characterBodySO);
			DestroyImmediate(characterBodySO, true); // 완전히 메모리에서 제거
			UnityEditor.AssetDatabase.SaveAssets();

			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();
		}

		public void RemoveBody(CharacterElementInfo bodyInfo)
		{
			CharacterBodySO characterBodySO = FindBody(bodyInfo);
			RemoveBody(characterBodySO);
		}
#endif
	}
}
