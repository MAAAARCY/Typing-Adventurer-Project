using UnityEngine;
using UnityEngine.UI;
using Enemies.Stage1;

namespace Managers
{
    public class ResetQuestionManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Questions;
        [SerializeField] private GameObject[] questions;
        [SerializeField] private GameObject[] choice_mark;
        [SerializeField] private TypingProcess TP;
        [SerializeField] private EyeAttackAlgorithm EAA;
        [SerializeField] private PlayerHPManager PHM;

        public void ResetQuestions()
        {
            EAA.AttackChange();
            TP.Damage_Player();

            for (int q_i = 0; q_i < Questions.Length; q_i++)
            {
                this.choice_mark[q_i].SetActive(false);
                this.Questions[q_i].SetActive(false);
            }
        }
    }
}