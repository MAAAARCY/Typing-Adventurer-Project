using UnityEngine;

namespace Managers
{
    public class BattleBGMManager : SettingManager
    {
        [SerializeField] private AudioClip[] BGM;
        private AudioSource BGMSource;

        void Start()
        {
            this.BGMSource = this.GetComponent<AudioSource>();
            this.BGMSource.clip = BGM[0];
            this.BGMSource.Stop();
        }

        public void BGMStart()
        {
            this.BGMSource.Play();
        }
        public void BGMStop()
        {
            this.BGMSource.Stop();
        }
        public void GameClearBGMPlay()
        {
            this.BGMSource.PlayOneShot(this.BGM[1]);
        }
    }
}