using Crogen.CrogenDialogue.Editor.UTIL;
using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.CharacterPreviewWindows
{
    public class CharacterElementView : Button
    {
        public event Action<string> OnRenameEvent;
        public event Action<Sprite> OnSpriteChanged;

        public CharacterElementView(string title, Sprite sprite)
        {
			style.backgroundColor = ColorPalette.buttonColor;
            style.maxHeight = 25;
            style.minHeight = 25;

            TextField textField = new();
			textField.isDelayed = true;
            textField.value = title;
			textField.maxLength = 16;
            textField.style.flexGrow = 1;
            textField.RegisterValueChangedCallback(evt => OnRenameEvent?.Invoke(evt.newValue));
            Add(textField);

            ObjectField spriteField = new();
            spriteField.objectType = typeof(Sprite);
            spriteField.value = sprite;
			spriteField.RegisterValueChangedCallback(evt => OnSpriteChanged?.Invoke(evt.newValue as Sprite));
			Add(spriteField);

            style.flexDirection = FlexDirection.Row;
        }
    }
}
