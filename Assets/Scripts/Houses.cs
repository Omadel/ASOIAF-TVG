using UnityEngine;

namespace ASOIAF {
    [CreateAssetMenu(fileName = "Houses", menuName = "ASOIAF/Houses/HousesStatic")]
    public class Houses : StaticScriptableObject {
        public static HouseData[] GetHouses { get; private set; }
        [SerializeField, ForceDebugMode] private HouseData[] houses;

        private void OnValidate() {
            GetHouses = houses;
        }

        public override void StaticSetup() {
            OnValidate();
        }
    }

    public enum HousesEnum {
        Stark,
        Lannister,
    }
}