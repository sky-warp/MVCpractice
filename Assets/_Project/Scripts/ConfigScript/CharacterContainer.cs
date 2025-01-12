using UnityEngine;
using Model;

namespace ConfigScript
{
    [CreateAssetMenu(fileName = "CharacterContainer", menuName = "Create Character Container/CharacterContainer")]
    public class CharacterContainer : ScriptableObject
    {
        [field: SerializeField] public Character Value { get; private set; }
    }
}