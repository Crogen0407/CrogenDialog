using Crogen.CrogenDialogue.Character;
using Crogen.CrogenDialogue.Editor.CharacterPreviewWindows;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.UTIL.CharacterPreviewWindows
{
    public static class CharacterElementCreator
    {
		public static CharacterElementView DrawBodyElementView(CharacterElementInfo characterElementInfo, CharacterPreviewWindow characterPreviewWindow)
		{
			return DrawCharacterElementView(
				characterPreviewWindow.SelectedBaseSO,
				characterElementInfo,
				characterPreviewWindow.BodyScrollView,
				characterPreviewWindow,
				() => characterPreviewWindow.SelectedBodySO = characterPreviewWindow.SelectedBaseSO.FindBody(characterElementInfo),
				newValue => characterPreviewWindow.CharacterPreview.SetBodyVisual(newValue));
		}

		public static CharacterElementView DrawClothseElementView(CharacterElementInfo characterElementInfo, CharacterPreviewWindow characterPreviewWindow)
		{
			return DrawCharacterElementView(
				characterPreviewWindow.SelectedBaseSO,
				characterElementInfo,
				characterPreviewWindow.ClothseScrollView,
				characterPreviewWindow,
				() => characterPreviewWindow.SelectedClothseInfo = characterElementInfo,
				newValue => characterPreviewWindow.CharacterPreview.SetClothseVisual(newValue));
		}

		public static CharacterElementView DrawFaceElementView(CharacterElementInfo characterElementInfo, CharacterPreviewWindow characterPreviewWindow)
		{
			return DrawCharacterElementView(
				characterPreviewWindow.SelectedBaseSO,
				characterElementInfo,
				characterPreviewWindow.FaceScrollView,
				characterPreviewWindow,
				() => characterPreviewWindow.SelectedFaceInfo = characterElementInfo,
				newValue => characterPreviewWindow.CharacterPreview.SetFaceVisual(newValue));
		}

		private static CharacterElementView DrawCharacterElementView(CharacterSO characterSO, CharacterElementInfo characterElementInfo, ScrollView scrollView, CharacterPreviewWindow characterPreviewWindow, Action clicked = null, Action<Sprite> spriteChanged = null)
		{
			var newElementView = new CharacterElementView(characterElementInfo.name, characterElementInfo.sprite);

			newElementView.clicked += () => clicked?.Invoke();

			newElementView.OnRenameEvent += newValue => {
				characterElementInfo.name = newValue;
				EditorUtility.SetDirty(characterSO);
				AssetDatabase.SaveAssets();
			};
			newElementView.OnSpriteChanged += newValue => {
				characterElementInfo.sprite = newValue;
				spriteChanged?.Invoke(newValue);
				EditorUtility.SetDirty(characterSO);
				AssetDatabase.SaveAssets();
			};

			scrollView.contentContainer.Add(newElementView);
			characterPreviewWindow.CharacterElementViews.TryAdd(characterElementInfo, newElementView);

			return newElementView;
		}
	}
}
