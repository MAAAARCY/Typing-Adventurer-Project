using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Managers;

namespace Enemies.Stage1
{
    public class EyeAttack : MonoBehaviour
    {
        [SerializeField] private GameObject Boss;
        [SerializeField] private GameObject DissolveBoss;
        [SerializeField] private GameObject Canvas;
        [SerializeField] private GameObject Arrow;
        [SerializeField] private GameObject Drill;
        [SerializeField] private PlayerHPManager PHM;
        [SerializeField] private ResetQuestionManager RQM;

        private Animator BossAnimator;
        private Animator EyeAnimator;
        private Animator UIAnimator;
        private Animator ArrowAnimator;
        private Animator DrillAnimator;

        public void ArrowAttackBegin()
        {
            BossAnimator.SetBool("attack1", true);
            ArrowAnimator.SetBool("attack1_arrow", true);
        }

        public void ArrowAttackEnd()
        {
            BossAnimator.SetBool("attack1", false);
            ArrowAnimator.SetBool("attack1_arrow", false);
            UIAnimator.SetBool("attack1", false);
            //this.Arrow.SetActive(false);
            PHM.DecreasePlayerHP();
            RQM.ResetQuestions();
        }

        public void ArrowAttackForcedEnd()
        {
            BossAnimator.SetBool("attack1", false);
            ArrowAnimator.SetBool("attack1_arrow", false);
            UIAnimator.SetBool("attack1", false);
            //this.Arrow.SetActive(false);
            RQM.ResetQuestions();
        }

        public void ColorChange_Arrow()
        {
            UIAnimator.SetBool("attack1", true);
        }

        public void DrillAttackBegin()
        {
            BossAnimator.SetBool("attack2", true);
            DrillAnimator.SetBool("attack2_drill", true);
        }

        public void DrillAttackEnd()
        {
            BossAnimator.SetBool("attack2", false);
            DrillAnimator.SetBool("attack2_drill", false);
            UIAnimator.SetBool("attack2", false);
            //this.Drill.SetActive(false);
            PHM.DecreasePlayerHP();
            RQM.ResetQuestions();
        }

        public void DrillAttackForcedEnd()
        {
            BossAnimator.SetBool("attack2", false);
            DrillAnimator.SetBool("attack2_drill", false);
            UIAnimator.SetBool("attack2", false);
            RQM.ResetQuestions();
        }

        public void ColorChange_Drill()
        {
            UIAnimator.SetBool("attack2", true);
        }

        public void AllAttackEnd()
        {
            ArrowAnimator.SetBool("attack1_arrow", false);
            DrillAnimator.SetBool("attack2_drill", false);
            this.Boss.SetActive(false);
            this.DissolveBoss.SetActive(true);
            EyeAnimator.SetTrigger("finish");
        }

        void Start()
        {
            BossAnimator = this.Boss.GetComponent<Animator>();
            EyeAnimator = this.DissolveBoss.GetComponent<Animator>();
            ArrowAnimator = this.Arrow.GetComponent<Animator>();
            DrillAnimator = this.Drill.GetComponent<Animator>();
            UIAnimator = this.Canvas.GetComponent<Animator>();
        }
    }
}