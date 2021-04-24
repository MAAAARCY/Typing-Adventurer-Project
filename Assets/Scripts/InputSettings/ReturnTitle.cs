using UnityEngine;
using UnityEngine.SceneManagement;

namespace InputSettings
{
    public class ReturnTitle : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Title Scene");
            }
        }
    }
}