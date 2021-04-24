using UnityEngine;
using UnityEngine.UI;
using Enemies.Stage1;

namespace Managers
{
    public class BossHPManager : MonoBehaviour
    {
        [SerializeField] private FadeInOutManager FIOM;
        //[SerializeField] private BossProcess BP;
        [SerializeField] private BattleBGMManager BM;
        [SerializeField] private Image BossHP;
        [SerializeField] private ResultManager RM;
        [SerializeField] private EyeAttack EA;

        public void DecreaseBossHP()
        {
            BossHP.fillAmount -= 0.1f;

            if (BossHP.fillAmount == 0.0f)
            {
                BM.BGMStop();
                //BP.FinishAnimation();
                EA.AllAttackEnd();
                RM.ResetUI();
            }
        }
    }
}