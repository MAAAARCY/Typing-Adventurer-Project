using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class PlayerHPManager : MonoBehaviour
    {
        [SerializeField] private FadeInOutManager FIOM;
        [SerializeField] private Image PlayerHP;
        [SerializeField] private ResultManager RM;

        public void DecreasePlayerHP()
        {
            PlayerHP.fillAmount -= 0.1f;
            FIOM.StartFade(1);

            if (PlayerHP.fillAmount == 0.0f)
            {
                RM.ShowGameOverResult();
            }
        }
    }
}