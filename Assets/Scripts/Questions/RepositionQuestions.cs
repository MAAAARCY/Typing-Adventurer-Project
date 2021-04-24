using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Questions
{
    public class RepositionQuestions : MonoBehaviour
    {
        [SerializeField] private GameObject[] Questions;
        [SerializeField] private GameObject[] questions;

        public void Reposition(int questions_count, Dictionary<char, int[]> position_dictionary)
        {
            for (int q_i = 0; q_i < questions_count; q_i++)
            {
                this.questions[q_i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                this.Questions[q_i].SetActive(true);
                this.Questions[q_i].GetComponent<RectTransform>().localPosition = new Vector3(position_dictionary['x'][q_i], position_dictionary['y'][q_i], 0);
            }
        }
    }
}
