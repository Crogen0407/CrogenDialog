using UnityEngine;

namespace Crogen.CrogenDialogue
{
    public class Storyteller : MonoBehaviour
    {
        [field: SerializeField] public StorytellerBaseSO StorytellerBase { get; private set; }
		[field: SerializeField] public bool StartGo { get; private set; } = false;

		private void Start()
		{
			if (StartGo)
				Go();
		}

		public void Go()
        {
            Debug.Assert(StorytellerBase.StartNode != null, "Start node is empty!");

            StorytellerBase.StartNode.Go(this);
		}
    }
}
