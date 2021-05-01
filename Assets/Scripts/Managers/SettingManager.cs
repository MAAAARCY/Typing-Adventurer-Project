using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SettingManager : MonoBehaviour
    {
        [SerializeField] private Slider BGMBar;
        private static bool isCallOnce = true;
        private static float BGMVolume = 0.0f;

        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Setting Scene")
            {
                if (BGMVolume != this.BGMBar.value)
                {
                    BGMVolume = this.BGMBar.value;
                }
            }
        }
    }
}