using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Questions;

namespace Enemies.Stage1
{
    public class EyeAttackAlgorithm : MonoBehaviour
    {
        [SerializeField] private GameObject Boss;
        [SerializeField] private QuestionsProvider QP;

        private Animator BossAnimator;

        private bool AttackBeginFlag;
        private bool AttackFirstFlag;
        private bool AttackChangeFlag;
        private Attack AttackStatus;
        private Dictionary<char, int[]> xypositions;

        public void AttackChange()
        {
            AttackChangeFlag = true;
        }

        public void ShowQuestions()
        {
            int[] xlst;
            int[] ylst;

            switch (AttackStatus) 
            {
                case Attack.Arrow:
                    xlst = new int[] { -330, 330 };
                    ylst = new int[] { 0, 0 };
                    xypositions['x'] = xlst;
                    xypositions['y'] = ylst;
                    QP.ProvideQuestions(2, xypositions);
                    break;
                case Attack.Drill:
                    xlst = new int[] { 0 };
                    ylst = new int[] { 0 };
                    xypositions['x'] = xlst;
                    xypositions['y'] = ylst;
                    QP.ProvideQuestions(1, xypositions);
                    break;
            }

            BossAnimator.SetBool($"attack{(int)AttackStatus}", false);
        }

        void Start()
        {
            BossAnimator = this.Boss.GetComponent<Animator>();

            xypositions = new Dictionary<char, int[]>();

            AttackBeginFlag = false;
            AttackFirstFlag = true;
            AttackChangeFlag = false;
        }

        void Update()
        {
            if (AttackFirstFlag)
            {
                FirstMotion();
                AttackFirstFlag = false;
            }
            if (AttackBeginFlag)
            {
                
                switch (AttackStatus) 
                {
                    case Attack.Arrow:
                        BossAnimator.SetBool("attack1", true);
                        break;
                    case Attack.Drill:
                        BossAnimator.SetBool("attack2", true);
                        break;
                }

                Debug.Log($"åªç›ÇÃçUåÇ:{AttackStatus}");
                AttackBeginFlag = false;
            }
            if (AttackChangeFlag)
            {
                AttackStatus = (Attack)Random.Range(1, 3);
                AttackBeginFlag = true;
                AttackChangeFlag = false;
            }
        }

        private async UniTask FirstMotion()
        {
            await UniTask.Delay((int)(3000));
            AttackStatus = (Attack)Random.Range(1, 3);
            Debug.Log($"åªç›ÇÃçUåÇ:{AttackStatus}");
            AttackBeginFlag = true;
        }
    }
}