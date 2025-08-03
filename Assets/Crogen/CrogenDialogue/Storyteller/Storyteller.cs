using Crogen.CrogenDialogue.UI;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
    public class Storyteller : MonoBehaviour
    {
        [field: SerializeField] public StorytellerBaseSO StorytellerBase { get; private set; }
		[field: SerializeField] public bool StartAndGo { get; private set; } = false;
		[field: SerializeField] private TalkPanel _talkPanel;
		[field: SerializeField] private ChoiceContainer _choiceContainer;

		// 강제 대화 완료
		public bool IsTalkComplete { get => _talkPanel.IsTalkComplete; set => _talkPanel.IsTalkComplete = value; }
		public bool IsChoiceComplete => _choiceContainer.IsChoiceComplete;
		public int ChoiceIndex => _choiceContainer.ChoiceIndex;

		private void Start()
		{
			if (StartAndGo)
				Go();
		}

		public bool Go()
        {
			if (StorytellerBase.StartNode == null)
			{
				Debug.LogError("Start node is empty!");
				return false;
			}
			if (StorytellerBase.IsError() == true)
			{
				Debug.LogError("StorytellerBase is error!");
				return false;
			}

			for (int i = 0; i < StorytellerBase.Billboard.Count; i++)
			{
				StorytellerBase.Billboard[i].SaveDefaultValues();
			}
			
			StorytellerBase.StartNode.Go(this);

			return true;
		}

		public void SetTalkText(string name, string talk)
		{
			_talkPanel.gameObject.SetActive(true);
			_talkPanel.SetTalkText(name, talk);
		}

		public void SetChoices(string[] choices)
		{
			_choiceContainer.SetActiveChoicePanels(true);
			_choiceContainer.SetChoices(choices);
		}

		public void SetActiveTalkPanel(bool active) => _talkPanel.gameObject.SetActive(active);
		public void SetActiveChoicePanels(bool active) => _choiceContainer.SetActiveChoicePanels(active);

		private void OnDestroy()
		{
			for (int i = 0; i < StorytellerBase.Billboard.Count; i++)
			{
				StorytellerBase.Billboard[i].ReturnToDefault();
			}
		}
	}
}
