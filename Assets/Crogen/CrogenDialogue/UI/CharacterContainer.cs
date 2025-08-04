using Crogen.CrogenDialogue.Character;
using System.Collections.Generic;
using UnityEngine;

namespace Crogen.CrogenDialogue.UI
{
    public class CharacterContainer : DialogueContainer
    {
		[SerializeField] CharacterPanel _characterPanelPrefab;
		public readonly Dictionary<CharacterSO, CharacterPanel> _characterPanelDictionary = new();

		public CharacterPanel EnableCharacter(CharacterSO character, string body, string clothse, string face)
		{
			if (_characterPanelDictionary.ContainsKey(character))
			{
				var characterPanel = _characterPanelDictionary[character];
				characterPanel.Initialize(character, body, clothse, face);
				characterPanel.SetActive(true);
				return characterPanel;
			}

			return CreateNewCharacterPanel(character, body, clothse, face);
		}

		public void DisableCharacter(CharacterSO character)
		{
			if (_characterPanelDictionary.ContainsKey(character))
			{
				_characterPanelDictionary[character].SetActive(false);
			}
		}

		private CharacterPanel CreateNewCharacterPanel(CharacterSO character, string body, string clothse, string face)
		{
			var newCharacterPanel = Instantiate(_characterPanelPrefab);
			newCharacterPanel.Initialize(character, body, clothse, face);
			newCharacterPanel.RectTransform.SetParent(RectTransform);
			newCharacterPanel.RectTransform.anchoredPosition = Vector2.zero;
			newCharacterPanel.SetActive(true);
			_characterPanelDictionary.Add(character, newCharacterPanel);

			return newCharacterPanel;
		}
	}
}
