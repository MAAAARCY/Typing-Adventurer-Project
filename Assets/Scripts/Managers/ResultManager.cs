using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ResultManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Questions;
        [SerializeField] private FadeInOutManager FIOM;
        [SerializeField] private BattleBGMManager BM;
        [SerializeField] private TypingProcess TP;
        [SerializeField] private GameObject Boss;
        [SerializeField] private GameObject Boss_for_animation;
        [SerializeField] private GameObject PlayerHP;
        [SerializeField] private GameObject BossHP;
        [SerializeField] private GameObject Result;
        [SerializeField] private Text ResultText;

        void Start()
        {
            this.Result.SetActive(false);
        }

        public void ShowClearResult()
        {
            FIOM.StartFade(5);
            this.Boss.SetActive(false);
            this.Boss_for_animation.SetActive(false);

            int[] result = TP.GetTypoAndTotal();
            this.ResultText.text = $" 総タイプ回数：{result[0]}\nミスタイプ数：{result[1]}\nEsc：タイトル画面へ　Space：ステージセレクト画面へ　Enter：リトライ";
            this.Result.SetActive(true);
            BM.GameClearBGMPlay();
        }

        public void ShowGameOverResult()
        {
            BM.BGMStop();
            for (int q_i = 0; q_i < Questions.Length; q_i++)
            {
                this.Questions[q_i].SetActive(false);
            }
            this.PlayerHP.SetActive(false);
            this.BossHP.SetActive(false);
            this.Boss.SetActive(false);
            FIOM.StartFade(2);
        }

        public void ResetUI()
        {
            for (int q_i = 0; q_i < Questions.Length; q_i++)
            {
                this.Questions[q_i].SetActive(false);
            }
            this.PlayerHP.SetActive(false);
            this.BossHP.SetActive(false);
        }
    }
}
