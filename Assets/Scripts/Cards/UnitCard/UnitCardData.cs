using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "UnitCard", menuName = "ASOIAF/Cards/UnitCard")]
    public class UnitCardData : ScriptableObject {
        public string Name => name;
        public int PointValue => pointValue;
        public HouseData House => house;
        public Sprite Character => character;
        public int Speed => speed;
        public int Defense => defense;
        public int Moral => moral;
        public UnitTypeData Type => type;
        public AttackData[] Attacks => attacks;
        public Ability[] Abilities => abilities;
        public string Lore => lore;
        public bool HasRequierement => hasRequierement;
        public string RequierementName => requierementName;
        public string RequierementText => requierementText;

        [SerializeField] private new string name;
        [SerializeField, Range(1, 10)] private int pointValue;
        [SerializeField] private HouseData house;
        [SerializeField, PreviewSprite] private Sprite character;
        [SerializeField, Range(1, 7)] private int speed;
        [SerializeField, Range(2, 6)] private int defense;
        [SerializeField, Range(2, 8)] private int moral;
        [SerializeField] private UnitTypeData type;
        [SerializeField] private AttackData[] attacks;
        [SerializeField] private Ability[] abilities;
        [SerializeField, TextArea(3, 12)] private string lore;
        [Header("Requierement")]
        [SerializeField] private bool hasRequierement;
        [SerializeField] private string requierementName;
        [SerializeField, TextArea(2, 3)] private string requierementText;
    }
}