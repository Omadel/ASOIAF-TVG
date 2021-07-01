using UnityEngine;
using UnityEngine.UI;

namespace ASOIAF {
    public class UnitCardAttack : MonoBehaviour {
        [SerializeField] private Image type, outline, background;
        [SerializeField] private GameObject[] ranks = new GameObject[3];
        [SerializeField] private TMPro.TextMeshProUGUI nameText;

        [Header("Ressources")]
        [SerializeField] private Sprite goldBackground;
        [SerializeField] private Sprite silverBackground;

        public void UpdateStats(AttackData attack, HouseData house) {
            nameText.text = attack.Name;
            switch(house.Material) {
                case Material.Gold:
                    type.sprite = attack.Type.Gold;
                    outline.sprite = attack.Type.GoldOutline;
                    background.sprite = goldBackground;
                    break;
                case Material.Silver:
                    type.sprite = attack.Type.Silver;
                    outline.sprite = attack.Type.SilverOutline;
                    background.sprite = silverBackground;
                    break;
            }
            for(int i = 0; i < ranks.Length; i++) {
                ranks[i].SetActive(i < attack.RanksDice.Length);
            }
            ranksText = new TMPro.TextMeshProUGUI[attack.RanksDice.Length];
            for(int i = 0; i < attack.RanksDice.Length; i++) {
                ranksText[i] = ranks[i].GetComponentInChildren<TMPro.TextMeshProUGUI>();
                ranksText[i].text = attack.RanksDice[i].ToString();
            }

        }

        private TMPro.TextMeshProUGUI[] ranksText;
    }
}
