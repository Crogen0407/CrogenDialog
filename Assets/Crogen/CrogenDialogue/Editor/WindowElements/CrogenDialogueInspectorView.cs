using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor
{
    public class CrogenDialogueInspectorView : VisualElement
    {
		private StorytellerBaseSO _storytellerBaseSO;

		public void Initialize(StorytellerBaseSO storytellerBaseSO)
        {
            this._storytellerBaseSO = storytellerBaseSO;


		}
    }
}
