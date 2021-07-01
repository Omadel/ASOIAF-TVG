using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "Ability", menuName = "ASOIAF/Abilities/Ability")]
    public class Ability : ScriptableObject {
        public string Name => name;
        public string Rule => rule;
        public AbilityTypeData Type => type;

        [SerializeField] private new string name;
        [SerializeField, TextArea(2, 5)] private string rule;
        [SerializeField] private AbilityTypeData type;
    }
}