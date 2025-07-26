using System;

namespace Crogen.CrogenDialogue
{
	[AttributeUsage(AttributeTargets.Field)]
	public class HideInEditorWindowAttribute : Attribute
    {
		public HideInEditorWindowAttribute() { }
	}
}
