using UnityEngine;
using UnityEngine.SceneManagement;

namespace InputSettings
{
    public class SceneSelect : MonoBehaviour
    {
        [SerializeField] private GameObject Boss;

        void Update()
        {
            if (!(Boss.activeSelf))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene("Title Scene");
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene("Select Scene");
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Stage1 Scene");
                }
            }
        }
    }
}