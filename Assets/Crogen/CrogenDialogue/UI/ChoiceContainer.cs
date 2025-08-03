using Crogen.CrogenDialogue.UI;
using System;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
    public class ChoiceContainer : MonoBehaviour
    {
		public bool IsChoiceComplete { get; private set; }
		public int ChoiceIndex { get; private set; }
        [SerializeField] private ChoicePanel[] _choicePanels;
		private CanvasGroup _canvasGroup;

		private void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();

			for (int i = 0; i < _choicePanels.Length; i++)
				_choicePanels[i].Initialize(this);
		}

		public void SetActiveChoicePanels(bool active)
		{
			if (active)
			{
				_canvasGroup.alpha = 1;
			}
			else
			{
				_canvasGroup.alpha = 0;
			}
			_canvasGroup.interactable = active;
			_canvasGroup.blocksRaycasts = active;
		}

		/// <summary>
		/// 선택지는 최대 5개입니다.
		/// </summary>
		public void SetChoices(string[] choices)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                _choicePanels[i].SetText(choices[i], i);
			}
		}

		public void ChoiseSelectComplete(int choiceIndex)
		{
			IsChoiceComplete = true;
			ChoiceIndex = choiceIndex;

			SetActiveChoicePanels(false);
		}
	}
}
