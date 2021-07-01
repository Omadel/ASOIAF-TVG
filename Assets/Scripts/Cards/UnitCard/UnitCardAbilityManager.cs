using UnityEngine;
using UnityEngine.UI;

namespace ASOIAF {
    public class UnitCardAbilityManager : MonoBehaviour, IUnitCardRenderer {
        [SerializeField] private GameObject abilityPrefab, separatorPrefab;

        public void UpdateVisual(UnitCardData unit) {
            if(unit == null) {
                SetRightAmountOfAbilityProfile(new Ability[0]);
                Resize();
                return;
            }

            SetRightAmountOfAbilityProfile(unit.Abilities);

            for(int i = 0; i < unit.Abilities.Length; i++) {
                abilities[i].UpdateStats(unit.Abilities[i], unit.House);
            }
            foreach(UnitCardSeparator separator in separators) {
                separator.UpdateVisual(unit);
            }

            Resize();

        }

        [ContextMenu("Resize")]
        private void Resize() {
            contentSizeFitters = GetComponentsInChildren<ILayoutController>();

            for(int i = 0; i < contentSizeFitters.Length; i++) {
                contentSizeFitters[i].SetLayoutVertical();
            }
        }

        private void SetRightAmountOfAbilityProfile(Ability[] attackDatas) {
            abilities = GetComponentsInChildren<UnitCardAbility>();
            separators = GetComponentsInChildren<UnitCardSeparator>();

            if(abilities.Length < attackDatas.Length) {
                int oldLenght = abilities.Length;
                abilities = new UnitCardAbility[attackDatas.Length];
                separators = new UnitCardSeparator[attackDatas.Length - 1];
                for(int i = oldLenght; i < attackDatas.Length; i++) {
                    if(i > 0) {
                        separators[i - 1] = GameObject.Instantiate(separatorPrefab, transform).GetComponent<UnitCardSeparator>();
                    }
                    abilities[i] = GameObject.Instantiate(abilityPrefab, transform).GetComponent<UnitCardAbility>();
                }
                abilities = GetComponentsInChildren<UnitCardAbility>();
                separators = GetComponentsInChildren<UnitCardSeparator>();
            }

            if(abilities.Length > attackDatas.Length) {
                for(int i = abilities.Length - 1; i > attackDatas.Length - 1; i--) {
                    if(Application.isPlaying) {
                        GameObject.Destroy(abilities[i].gameObject);
                        if(i > 0) {
                            GameObject.Destroy(separators[i - 1].gameObject);
                        }
                    } else {
                        GameObject.DestroyImmediate(abilities[i].gameObject);
                        if(i > 0) {
                            GameObject.DestroyImmediate(separators[i - 1].gameObject);
                        }
                    }

                }
                abilities = GetComponentsInChildren<UnitCardAbility>();
                separators = GetComponentsInChildren<UnitCardSeparator>();
            }
        }

        private UnitCardSeparator[] separators;
        private UnitCardAbility[] abilities;
        private ILayoutController[] contentSizeFitters;
    }
}