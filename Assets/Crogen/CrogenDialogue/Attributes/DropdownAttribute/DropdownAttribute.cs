using UnityEngine;

namespace Crogen.CrogenDialogue
{
	public class DropdownAttribute : PropertyAttribute
	{
		public string[] options;

		public DropdownAttribute(params string[] options)
		{
			this.options = options;
		}
	}
}
