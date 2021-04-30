using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SettingManager : MonoBehaviour
    {
        [SerializeField] private Slider BGMBar;

        protected static float BGMVolume = 0.5f;

        void Start()
        {
            BGMBar.value = TitleBGMManager.BGMVolume;
        }

        void Update()
        {
            if (SceneManager.GetActiveScene().name == "Setting Scene")
            {
                if (BGMVolume != BGMBar.value)
                {
                    BGMVolume = BGMBar.value;
                }
            }
        }
    }
}