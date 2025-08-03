using UnityEngine;
using UnityEngine.UI;

namespace Crogen.CrogenDialogue.UI
{
    public class CharacterPanel : MonoBehaviour
    {
		private CanvasGroup _canvasGroup;
		private Image _characterImage;
		private Image _clothseImage;

		private RectTransform _rectTransform;
		public RectTransform RectTransform 
		{ 
			get
			{
				_rectTransform ??= transform as RectTransform;
				return _rectTransform;
			}
		}

		private void Awake()
		{
			AddClothseImage();
			gameObject.AddComponent(typeof(CanvasRenderer));
			_canvasGroup = gameObject.AddComponent<CanvasGroup>();
			_characterImage = gameObject.AddComponent<Image>();
			_characterImage.preserveAspect = true;
		}

		private void AddClothseImage()
		{
			_clothseImage = new GameObject("ClothseImage", typeof(CanvasRenderer)).AddComponent<Image>();
			_clothseImage.rectTransform.SetParent(RectTransform);
			_clothseImage.rectTransform.anchorMax = Vector2.one;
			_clothseImage.rectTransform.anchorMin = Vector2.zero;
			_clothseImage.rectTransform.anchoredPosition = Vector2.zero;
			_clothseImage.rectTransform.sizeDelta = Vector2.zero;
			_clothseImage.preserveAspect = true;
		}

		public void SetActive(bool active)
		{
			if (active)
			{
				_canvasGroup.alpha = 1;
			}
			else
			{
				_canvasGroup.alpha = 0;
				StopAllCoroutines();
			}
			_canvasGroup.interactable = active;
			_canvasGroup.blocksRaycasts = active;
		}
    }
}
