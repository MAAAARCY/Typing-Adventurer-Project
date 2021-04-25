using UnityEngine;

namespace Managers
{
    public class GameStartManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Questions;
        [SerializeField] private GameObject Canvas;
        [SerializeField] private GameObject Boss;

        private Animator UIAnimator;

        void Start()
        {
            for (int i = 0; i < this.Questions.Length; i++)
            {
                this.Questions[i].SetActive(false);
            }

            UIAnimator = this.Canvas.GetComponent<Animator>();

            UIAnimator.SetBool("UIStartAnimation", true);
            UIAnimator.SetFloat("UIMoveSpeed", 0.0f);
        }
    }
}