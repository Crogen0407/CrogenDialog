using UnityEngine;

namespace Crogen.CrogenDialogue
{
	[RequireComponent(typeof(CanvasGroup))]
    public abstract class DialogueContainer : MonoBehaviour
    {
		private RectTransform _rectTransform;
		public RectTransform RectTransform
		{
			get
			{
				_rectTransform ??= transform as RectTransform;
				return _rectTransform;
			}
		}
		private CanvasGroup _canvasGroup;

		protected virtual void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
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
