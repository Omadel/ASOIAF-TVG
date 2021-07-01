using UnityEngine;
namespace ASOIAF {
    public class UnitCardAttackManager : MonoBehaviour, IUnitCardRenderer {
        [SerializeField] private GameObject attackPrefab;

        public void UpdateVisual(UnitCardData unit) {
            if(unit == null) {
                SetRightAmountOfAttackProfile(new AttackData[0]);
                return;
            }

            SetRightAmountOfAttackProfile(unit.Attacks);

            for(int i = 0; i < unit.Attacks.Length; i++) {
                attacks[i].UpdateStats(unit.Attacks[i], unit.House);
            }

        }

        private void SetRightAmountOfAttackProfile(AttackData[] attackProfiles) {
            attacks = GetComponentsInChildren<UnitCardAttack>();

            if(attacks.Length < attackProfiles.Length) {
                int oldLenght = attacks.Length;
                attacks = new UnitCardAttack[attackProfiles.Length];
                for(int i = oldLenght; i < attackProfiles.Length; i++) {
                    attacks[i] = GameObject.Instantiate(attackPrefab, transform).GetComponent<UnitCardAttack>();
                }
                attacks = GetComponentsInChildren<UnitCardAttack>();
            }

            if(attacks.Length > attackProfiles.Length) {
                for(int i = attacks.Length - 1; i > attackProfiles.Length - 1; i--) {
                    if(Application.isPlaying) {
                        GameObject.Destroy(attacks[i].gameObject);
                    } else {
                        GameObject.DestroyImmediate(attacks[i].gameObject);
                    }
                }
                attacks = GetComponentsInChildren<UnitCardAttack>();
            }
        }


        private UnitCardAttack[] attacks;
    }
}