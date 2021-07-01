using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "Attack", menuName = "ASOIAF/Stat/Attack")]
    public class AttackData : ScriptableObject {
        public string Name { get => name; }
        public AbilityTypeData Type { get => type; }
        public int[] RanksDice { get => ranksdice; }
        public int ToHitValue { get => tohitvalue; }

        [SerializeField] private new string name;
        [SerializeField] private AbilityTypeData type;
        [SerializeField, Min(0)] private int[] ranksdice = new int[3];
        [SerializeField, Range(2, 6)] private int tohitvalue = 4;
    }
    public enum AttackType { Melee, LongRanged, ShortRanged }
}