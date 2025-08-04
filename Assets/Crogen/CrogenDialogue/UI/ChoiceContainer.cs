using Crogen.CrogenDialogue.UI;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
    public class ChoiceContainer : DialogueContainer
	{
		public bool IsChoiceComplete { get; private set; }
		public int ChoiceIndex { get; private set; }
        [SerializeField] private ChoiceElementPanel[] _choicePanels;

		protected override void Awake()
		{
			base.Awake();

			for (int i = 0; i < _choicePanels.Length; i++)
				_choicePanels[i].Initialize(this);
		}

		/// <summary>
		/// 선택지는 최대 5개입니다.
		/// </summary>
		public void SetChoices(string[] choices)
        {
			SetActive(true);
			IsChoiceComplete = false;
			for (int i = 0; i < choices.Length; i++)
            {
				_choicePanels[i].gameObject.SetActive(true);
				_choicePanels[i].SetText(choices[i], i);
			}

			for (int i = choices.Length; i < _choicePanels.Length; i++)
				_choicePanels[i].gameObject.SetActive(false);
		}

		public void ChoiseSelectComplete(int choiceIndex)
		{
			IsChoiceComplete = true;
			ChoiceIndex = choiceIndex;

			SetActive(false);
		}
	}
}
