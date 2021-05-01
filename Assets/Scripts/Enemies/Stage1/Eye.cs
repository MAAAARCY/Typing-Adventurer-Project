using Cysharp.Threading.Tasks;
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
        [SerializeField] private AudioClip[] SE;

        private Animator BossAnimator;
        private Animator UIAnimator;
        private AudioSource SESource;

        void Start()
        {
            this.BossAnimator = this.Boss.GetComponent<Animator>();
            this.UIAnimator = this.Canvas.GetComponent<Animator>();
            this.SESource = this.GetComponent<AudioSource>();

            this.BossAnimator.SetFloat("NormalSpeed", 0.0f);

            StartAnimation();

            this.Boss.SetActive(false);
        }

        private async UniTask StartAnimation()
        {
            await UniTask.Delay((int)(1100));

            FIOM.StartFade(3);

            await UniTask.Delay((int)(4500));

            this.DissolveBoss.SetActive(false);
            this.Boss.SetActive(true);

            this.BossAnimator.SetFloat("NormalSpeed", 1.0f);
            FIOM.StartFade(4);
            this.SESource.PlayOneShot(this.SE[0]);

            await UniTask.Delay((int)(500));

            this.SESource.PlayOneShot(this.SE[1]);
            this.UIAnimator.SetFloat("UIMoveSpeed", 1.0f);

            await UniTask.Delay((int)(4000));

            this.UIAnimator.SetBool("UIStartAnimation", false);
        }
    }
}