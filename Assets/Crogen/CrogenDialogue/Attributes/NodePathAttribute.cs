using System;

namespace Crogen.CrogenDialogue.Assets.Crogen.CrogenDialogue.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class NodePathAttribute : Attribute
    {
        public readonly string path;
        public NodePathAttribute(string path) { this.path = path; }
    }
}
