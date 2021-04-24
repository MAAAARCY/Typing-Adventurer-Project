using UnityEngine;

namespace Managers
{
    public class GameStartManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Questions;
        [SerializeField] private GameObject Canvas;
        [SerializeField] private GameObject Boss;
        private Animator ui_animator;

        void Start()
        {
            for (int i = 0; i < this.Questions.Length; i++)
            {
                this.Questions[i].SetActive(false);
            }

            ui_animator = this.Canvas.GetComponent<Animator>();

            ui_animator.SetBool("UIStartAnimation", true);
            ui_animator.SetFloat("UIMoveSpeed", 0.0f);
            /*
            g_animator = this.Gankyu.GetComponent<Animator>();
            b_animator = this.Boss.GetComponent<Animator>();

            g_animator.SetFloat("NormalSpeed", 0.0f);
            */
            //this.Boss.SetActive(false);
        }
    }
}