using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crogen.CrogenDialogue.UI
{
    public class ChoiceElementPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _choiceText;
        [SerializeField] private Button _button;
        private ChoiceContainer _choiceContainer;
        private int _choiceIndex;

		public void Initialize(ChoiceContainer choiceContainer)
        {
            this._choiceContainer = choiceContainer;
			_button.onClick.AddListener(HandleChoiseSelectComplete);
		}

		public void SetText(string text, int index)
        {
            _choiceIndex = index;
            _choiceText.text = text;
		}

		private void HandleChoiseSelectComplete()
			=> _choiceContainer.ChoiseSelectComplete(_choiceIndex);
	}
}
