using UnityEngine;
using UnityEngine.UIElements;

namespace Crogen.CrogenDialogue.Editor.CharacterPreviewWindows
{
	[UxmlElement]
	public partial class CharacterPreview : VisualElement
	{
		private VisualElement _bodyPreview;
		private VisualElement _clothsePreview;
		private VisualElement _facePreview;

		public CharacterPreview()
		{
			style.backgroundColor = new Color(0.1647059f, 0.1647059f, 0.1647059f, 1f);
			style.flexGrow = 1f;

			_bodyPreview = new VisualElement();
			_bodyPreview.name = "BodyPreview";
			_bodyPreview.AddToClassList("preview");

			_clothsePreview = new VisualElement();
			_clothsePreview.name = "ClothsePreview";
			_clothsePreview.AddToClassList("preview");

			_facePreview = new VisualElement();
			_facePreview.name = "FacePreview";
			_facePreview.AddToClassList("preview");

			Add(_bodyPreview);
			_bodyPreview.Add(_clothsePreview);
			_clothsePreview.Add(_facePreview);
		}

		public void SetBodyVisual(Sprite sprite)
		{
			_bodyPreview.style.backgroundImage = new(sprite);
		}

		public void SetClothseVisual(Sprite sprite)
		{
			_clothsePreview.style.backgroundImage = new(sprite);
		}

		public void SetFaceVisual(Sprite sprite)
		{
			_facePreview.style.backgroundImage = new(sprite);
		}
	}
}
