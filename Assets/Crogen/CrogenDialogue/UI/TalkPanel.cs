using System.Collections;
using TMPro;
using UnityEngine;

namespace Crogen.CrogenDialogue.UI
{
    public class TalkPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _talkText;

        public void SetNameText(string text)
        {
            this._nameText.text = text;
		}

        public void SetTalkText(string text)
        {
            StopAllCoroutines();
            StartCoroutine(CoroutineSetTalkText(text));
        }

        private IEnumerator CoroutineSetTalkText(string text)
        {
            _talkText.text = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                _talkText.text += text[i];
                yield return new WaitForSeconds(0.1f);
            }
		}
    }
}
