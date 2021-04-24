using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Questions
{
    public class ResizeQuestionsScale : MonoBehaviour
    {
        [SerializeField] private GameObject[] questions;
        [SerializeField] private GameObject[] romaji;

        public void Resize()
        {
            for (int i = 0; i < questions.Length; i++)
            {
                if (this.romaji[i].activeSelf)
                {
                    int scale = this.romaji[i].GetComponent<Text>().text.Length;
                    switch (scale)
                    {
                        case int s_j when s_j <= 6:
                            this.questions[i].GetComponent<RectTransform>().localScale = new Vector3(3, 1, 1);
                            break;
                        case int s_j when s_j <= 10:
                            this.questions[i].GetComponent<RectTransform>().localScale = new Vector3(4, 1, 1);
                            break;
                        default:
                            this.questions[i].GetComponent<RectTransform>().localScale = new Vector3(5, 1, 1);
                            break;
                    }
                }
            }
        }
    }
}