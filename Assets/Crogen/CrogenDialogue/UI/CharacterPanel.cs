using Crogen.CrogenDialogue.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Crogen.CrogenDialogue.UI
{
    public class CharacterPanel : MonoBehaviour
    {
		private CanvasGroup _canvasGroup;
		public CanvasGroup CanvasGroup { get => _canvasGroup ??= GetComponent<CanvasGroup>(); set => _canvasGroup = value; }
		[SerializeField] private Image _bodyImage;
		[SerializeField] private Image _clothseImage;
		[SerializeField] private Image _faceImage;

		private RectTransform _rectTransform;
		public RectTransform RectTransform 
		{ 
			get
			{
				_rectTransform ??= transform as RectTransform;
				return _rectTransform;
			}
		}

		private CharacterBodySO _characterBodySO;

		public void Initialize(CharacterSO character, string body, string clothse, string face)
		{
			var bodyData = character.FindBody(body);

			if (bodyData != null)
				_characterBodySO = bodyData;

			if (_characterBodySO == null) return;
			_bodyImage.sprite = _characterBodySO.BodyInfo.sprite;

			if (string.IsNullOrEmpty(clothse) == false)
			{
				var findSprite = _characterBodySO.FindClothse(clothse)?.sprite;
				if (findSprite != null)
					_clothseImage.sprite = findSprite;
			}
			if (string.IsNullOrEmpty(face) == false)
			{
				var findSprite = _characterBodySO.FindFace(face)?.sprite;
				if (findSprite != null)
					_faceImage.sprite = findSprite;
			}
		}

		public void SetActive(bool active)
		{
			if (active)
			{
				CanvasGroup.alpha = 1;
			}
			else
			{
				CanvasGroup.alpha = 0;
				StopAllCoroutines();
			}
			CanvasGroup.interactable = active;
			CanvasGroup.blocksRaycasts = active;
		}
    }
}
