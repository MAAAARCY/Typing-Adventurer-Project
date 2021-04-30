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
        [SerializeField] private AudioClip SE;

        private AudioSource SESource;
        private int NowHP = 100;

        void Start()
        {
            this.SESource = this.GetComponent<AudioSource>();
        }

        public void DecreasePlayerHP()
        {
            PlayerHP.fillAmount -= 0.1f;
            NowHP = NowHP - 10;
            PlayerHPText.text = $"{NowHP}/100";
            FIOM.StartFade(1);
            SESource.PlayOneShot(SE);

            if (PlayerHP.fillAmount == 0.0f)
            {
                RM.ShowGameOverResult();
            }
        }
    }
}