using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class PlayerHPManager : MonoBehaviour
    {
        [SerializeField] private FadeInOutManager FIOM;
        [SerializeField] private Image PlayerHP;
        [SerializeField] private Text PlayerHPText;
        [SerializeField] private ResultManager RM;

        private int NowHP = 100;

        public void DecreasePlayerHP()
        {
            PlayerHP.fillAmount -= 0.1f;
            NowHP = NowHP - 10;
            PlayerHPText.text = $"{NowHP}/100";
            FIOM.StartFade(1);

            if (PlayerHP.fillAmount == 0.0f)
            {
                RM.ShowGameOverResult();
            }
        }
    }
}