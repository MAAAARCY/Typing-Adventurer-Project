using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Questions
{
    public class QuestionsProvider : MonoBehaviour
    {
        [SerializeField] private GameObject[] Questions;

        public void ProvideQuestions(int questions_count ,Dictionary<char, int[]> position_dictionary)
        {
            for (int q_i = 0; q_i < questions_count; q_i++)
            {
                this.Questions[q_i].SetActive(true);
                this.Questions[q_i].GetComponent<RectTransform>().localPosition = new Vector3(position_dictionary['x'][q_i], position_dictionary['y'][q_i], 0);
            }
        }
    }
}