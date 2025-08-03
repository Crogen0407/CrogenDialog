using UnityEngine;

namespace Crogen.CrogenDialogue.Character
{
    [CreateAssetMenu(fileName = nameof(CharacterSO), menuName = "CrogenDialogue/CharacterSO")]
    public class CharacterSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite BaseSprite { get; private set; }
    }
}
