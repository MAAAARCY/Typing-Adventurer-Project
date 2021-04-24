using UnityEngine;
using UnityEngine.SceneManagement;

namespace InputSettings
{
    public class DoubleTapReturnTitle : MonoBehaviour
    {
        private bool isDoubleTapStart;
        private float doubleTapTime;

        void Start()
        {
            isDoubleTapStart = false;
            doubleTapTime = 0.0f;
        }

        void Update()
        {
            if (isDoubleTapStart)
            {
                doubleTapTime += Time.deltaTime;
                if (doubleTapTime < 0.3f)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene("Select Scene");
                    }
                }
                else
                {
                    isDoubleTapStart = false;
                    doubleTapTime = 0.0f;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isDoubleTapStart = true;
                }
            }
        }
    }
}
