using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class FadeInOutManager : MonoBehaviour
    {
        [SerializeField] private Image FadeImage;
        [SerializeField] private Text GameOverText;
        [SerializeField] private Text GameStartText;
        [SerializeField] private Text GameClearText;
        [SerializeField] private Text DangerText;

        private int NowStatus;
        private float alfa;
        private float fade_speed;

        void Start()
        {
            FadeImage.color = new Color(0, 0, 0, 255);
            GameStartText.color = new Color(255, 255, 255, 255);
            NowStatus = 0;
            alfa = 1.0f;
            fade_speed = 0.1f;
        }

        void Update()
        {
            switch (NowStatus)
            {
                case 1:
                    alfa += fade_speed;
                    FadeImage.color = new Color(255, 0, 0, alfa);
                    if (alfa >= 0.5f)
                    {
                        fade_speed = -0.1f;
                    }
                    if (alfa <= 0.0f)
                    {
                        alfa = 0.0f;
                        fade_speed = 0.1f;
                        NowStatus = 0;
                    }
                    break;
                case 2:
                    alfa += 0.01f;
                    FadeImage.color = new Color(0, 0, 0, alfa);
                    GameOverText.color = new Color(255, 0, 0, alfa);
                    if (alfa >= 1.0f)
                    {
                        alfa = 1.0f;
                        NowStatus = 0;
                    }
                    break;
                case 3:
                    alfa -= 0.01f;
                    FadeImage.color = new Color(0, 0, 0, alfa);
                    GameStartText.color = new Color(255, 255, 255, alfa);

                    //alfa -= 0.05f;
                    if (alfa <= 0.0f)
                    {
                        alfa = 1.0f;
                        NowStatus = 0;
                    }
                    break;
                case 4:
                    alfa -= 0.01f;
                    DangerText.color = new Color(255, 0, 0, alfa);

                    if (alfa <= 0.0f)
                    {
                        alfa = 0.0f;
                        NowStatus = 0;
                    }
                    break;
                case 5:
                    GameClearText.text = "Stage Clear!!";
                    alfa += 0.01f;
                    FadeImage.color = new Color(0, 0, 0, alfa);
                    if (alfa >= 0.9f)
                    {
                        alfa = 0.9f;
                        NowStatus = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        public void StartFade(int number)
        {
            NowStatus = number;
        }
    }
}