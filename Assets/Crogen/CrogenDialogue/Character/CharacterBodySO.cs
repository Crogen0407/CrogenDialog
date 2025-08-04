using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue.Character
{
    [Serializable]
    public class CharacterElementInfo
    {
        public string name;
        public Sprite sprite;
    }

	public class CharacterBodySO : ScriptableObject
    {
		[field: SerializeField] public CharacterElementInfo BodyInfo { get; set; } = new();
		[field: SerializeField] public List<CharacterElementInfo> ClothseList { get; set; } = new();
		[field: SerializeField] public List<CharacterElementInfo> FaceList { get; set; } = new();

		public CharacterElementInfo FindClothse(string clothseName)
			=> ClothseList.Find(x => x.name.Equals(clothseName));

		public CharacterElementInfo FindFace(string faceName)
			=> FaceList.Find(x => x.name.Equals(faceName));


#if UNITY_EDITOR
		public CharacterElementInfo AddNewClothse()
		{
			var chothseData = new CharacterElementInfo();
			ClothseList.Add(chothseData);

			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();

			return chothseData;
		}

		public void RemoveClothse(CharacterElementInfo clothseInfo)
		{
			if (ClothseList.Remove(clothseInfo) == false) return;

			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();
		}

		public CharacterElementInfo AddNewFace()
		{
			var faceData = new CharacterElementInfo();
			FaceList.Add(faceData);

			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();

			return faceData;
		}

		public void RemoveFace(CharacterElementInfo clothseInfo)
		{
			if (FaceList.Remove(clothseInfo) == false) return;

			UnityEditor.EditorUtility.SetDirty(this);
			UnityEditor.AssetDatabase.SaveAssets();
		}
#endif
	}
}
