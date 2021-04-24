using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Managers;

namespace Enemies.Stage1
{
    public class Eye : MonoBehaviour
    {
        [SerializeField] private GameObject Boss;
        [SerializeField] private GameObject DissolveBoss;
        [SerializeField] private GameObject Canvas;
        [SerializeField] private FadeInOutManager FIOM;

        private Animator BossAnimator;
        private Animator UIAnimator;

        void Start()
        {
            BossAnimator = this.Boss.GetComponent<Animator>();
            UIAnimator = this.Canvas.GetComponent<Animator>();

            BossAnimator.SetFloat("NormalSpeed", 0.0f);

            StartAnimation();

            this.Boss.SetActive(false);
        }

        private async Task StartAnimation()
        {
            await Task.Run(() => Thread.Sleep(1100));

            FIOM.StartFade(3);
            
            await Task.Run(() => Thread.Sleep(4000));
            
            this.DissolveBoss.SetActive(false);
            this.Boss.SetActive(true);
            Debug.Log(Boss.activeSelf);
            BossAnimator.SetFloat("NormalSpeed", 1.0f);
            FIOM.StartFade(4);
            UIAnimator.SetFloat("UIMoveSpeed", 1.0f);
            
            await Task.Run(() => Thread.Sleep(4000));
            
            UIAnimator.SetBool("UIStartAnimation", false);
        }
    }
}