using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "DeckCard", menuName = "ASOIAF/Cards/DeckCard")]
    public class DeckCardData : ScriptableObject {
        public HouseData House => house;
        [SerializeField] private HouseData house;
        public string CardName;
        [TextArea(2, 4)] public string ActivationText;
        [TextArea(4, 8)] public string RuleText;
    }
}