using System.Collections;
using TMPro;
using UnityEngine;

namespace Crogen.CrogenDialogue.UI
{
    public class TalkContainer : DialogueContainer
	{
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _talkText;

        private bool _isTalkComplete;
		public bool IsTalkComplete 
        { 
            get => _isTalkComplete;
            set 
            {
                StopAllCoroutines();
                _talkText.text = _talk;
				_isTalkComplete = value;
            }
        }

        private string _talk;

		public void SetTalkText(string name, string talk)
        {
            SetActive(true);
            IsTalkComplete = false;
			this._nameText.text = name;
            this._talk = talk;

			StopAllCoroutines();
            StartCoroutine(CoroutineSetTalkText(talk));
        }

        private IEnumerator CoroutineSetTalkText(string talk)
        {
            _talkText.text = string.Empty;

            for (int i = 0; i < talk.Length; i++)
            {
                yield return new WaitForSeconds(0.1f);
                _talkText.text += talk[i];
			}

            IsTalkComplete = true;
		}
    }
}
