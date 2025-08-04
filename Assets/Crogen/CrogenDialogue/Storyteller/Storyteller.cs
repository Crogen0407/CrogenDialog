using Crogen.CrogenDialogue.Character;
using Crogen.CrogenDialogue.UI;
using System;
using UnityEngine;

namespace Crogen.CrogenDialogue
{
    public class Storyteller : MonoBehaviour
    {
        [field: SerializeField] public StorySO StorytellerBase { get; private set; }
		[field: SerializeField] public bool StartAndGo { get; private set; } = false;

		// UI
		[field: SerializeField] public TalkContainer TalkContainer { get; private set; }
		[field: SerializeField] public ChoiceContainer ChoiceContainer { get; private set; }
		[field: SerializeField] public CharacterContainer CharacterCotainer { get; private set; }

		// 강제 대화 완료
		public bool IsTalkComplete { get => TalkContainer.IsTalkComplete; set => TalkContainer.IsTalkComplete = value; }
		public int ChoiceIndex => ChoiceContainer.ChoiceIndex;

		private void Start()
		{
			if (StartAndGo)
				Go();
		}

		public bool Go()
        {
			if (CheckError() == true) return false;

			for (int i = 0; i < StorytellerBase.Billboard.Count; i++)
			{
				StorytellerBase.Billboard[i].SaveDefaultValues();
			}

			UIInitialize();

			StorytellerBase.StartNode.Go(this);

			return true;
		}

		private bool CheckError()
		{
			if (StorytellerBase.StartNode == null)
			{
				Debug.LogError("Start node is empty!");
				return true;
			}
			if (StorytellerBase.IsError() == true)
			{
				Debug.LogError("StorytellerBase is error!");
				return true;
			}

			return false;
		}

		private void UIInitialize()
		{
			ChoiceContainer.SetActive(false);
		}

		private void OnDestroy()
		{
			for (int i = 0; i < StorytellerBase.Billboard.Count; i++)
			{
				StorytellerBase.Billboard[i].ReturnToDefault();
			}
		}
	}
}
