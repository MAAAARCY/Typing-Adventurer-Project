using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class TitleBGMManager : SettingManager
    {
        [SerializeField] private AudioClip BGM;
        private AudioSource BGMSource;
        private static bool DontReloadFlag = true;
        private static bool isCalledOnce = true;

        protected static float BGMVolume;

        void Start()
        {
            this.BGMSource = this.GetComponent<AudioSource>();
            this.BGMSource.clip = BGM;
            this.BGMSource.volume = SettingManager.BGMVolume;

            BGMVolume = SettingManager.BGMVolume;
            Debug.Log(BGMVolume);

            if (DontReloadFlag)
            {
                DontDestroyOnLoad(this);
                BGMSource.Play();
                DontReloadFlag = false;
            }
        }

        void Update()
        {
            if (!(DontReloadFlag) && SceneManager.GetActiveScene().name == "Stage1 Scene" && isCalledOnce)
            {
                BGMSource.Stop();
                isCalledOnce = false;
                Debug.Log("BGM1í‚é~");
            }
            if (!(DontReloadFlag) && SceneManager.GetActiveScene().name != "Stage1 Scene" && !(isCalledOnce))
            {
                BGMSource.Play();
                isCalledOnce = true;
                Debug.Log("BGMçƒäJén");
            }
            if (BGMSource.volume != SettingManager.BGMVolume)
            {
                BGMSource.volume = SettingManager.BGMVolume;
            }
        }
    }
}