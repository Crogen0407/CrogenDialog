using Crogen.CrogenDialogue.Character;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue.UI
{
    public class CharacterContainer : DialogueContainer
    {
		public readonly Dictionary<CharacterSO, CharacterPanel> _characterPanelDictionary = new();

		public CharacterPanel EnableCharacter(CharacterSO character)
		{
			if (_characterPanelDictionary.ContainsKey(character))
			{
				var characterPanel = _characterPanelDictionary[character];
				characterPanel.SetActive(true);
				return characterPanel;
			}

			return CreateNewCharacterPanel(character);
		}

		public void DisableCharacter(CharacterSO character)
		{
			if (_characterPanelDictionary.ContainsKey(character))
			{
				_characterPanelDictionary[character].SetActive(false);
			}
		}

		private CharacterPanel CreateNewCharacterPanel(CharacterSO character)
		{
			var newCharacterPanel = new GameObject($"{character.name}UI").AddComponent<CharacterPanel>();
			newCharacterPanel.RectTransform.SetParent(RectTransform);
			newCharacterPanel.SetActive(true);
			_characterPanelDictionary.Add(character, newCharacterPanel);

			return newCharacterPanel;
		}
	}
}
