using System;
using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "House", menuName = "ASOIAF/Houses/House")]
    public class HouseData : ScriptableObject {
        public Sprite DeckFront => deckFront;
        public Sprite AttachementFront => attachementFront;
        public Sprite UnitFront => unitFront;
        public Sprite UnitBack => unitBack;
        public Sprite AbilityMask => abilityMask;
        public Sprite SeparatorSprite => separatorSprite;
        public Sprite RequierementNameImage => requierementNameImage;
        public Sprite RequierementImage => requierementImage;
        public Color Color => color;
        public Material Material => material;

        [SerializeField, PreviewSprite(50)]
        private Sprite deckFront,
            attachementFront,
            unitFront, unitBack,
            abilityMask, separatorSprite,
            requierementNameImage, requierementImage;
        [SerializeField] private Color color;
        [SerializeField] private Material material;

        public static implicit operator HousesEnum(HouseData house) => (HousesEnum)Array.IndexOf(Houses.GetHouses, house);
    }

    public enum Material {
        Gold,
        Silver
    }
}