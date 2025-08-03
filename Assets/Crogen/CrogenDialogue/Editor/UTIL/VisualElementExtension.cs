using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.UTIL
{
    public static class VisualElementExtension
    {
		public static void MoveToBottom(this VisualElement target, VisualElement parent)
		{
			if (target.parent != parent)
				return;

			parent.Remove(target);
			parent.Add(target); // 맨 마지막 자식으로 다시 추가
		}
	}
}
