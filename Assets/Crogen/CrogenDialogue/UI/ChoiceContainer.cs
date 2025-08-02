using Crogen.CrogenDialogue.UI;
using System;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
    public class ChoiceContainer : MonoBehaviour
    {
		public event Action OnChoiseSelectCompleteEvent;
        [SerializeField] private ChoicePanel[] _choisePanels;

		private void Awake()
		{
			for (int i = 0; i < _choisePanels.Length; i++)
				_choisePanels[i].Initialize(this);
		}

		/// <summary>
		/// 선택지는 최대 5개입니다.
		/// </summary>
		public void SetChoises(string[] choises)
        {
            for (int i = 0; i < choises.Length; i++)
            {
                _choisePanels[i].SetText(choises[i]);
			}
		}

		public void ChoiseSelectComplete()
		{
			OnChoiseSelectCompleteEvent?.Invoke();
		}
	}
}
