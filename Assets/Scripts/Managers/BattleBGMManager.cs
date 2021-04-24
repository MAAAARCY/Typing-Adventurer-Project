using UnityEngine;

namespace Managers
{
    public class BattleBGMManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] BGM;
        private AudioSource BGMSource;

        void Start()
        {
            this.BGMSource = this.GetComponent<AudioSource>();
            this.BGMSource.clip = BGM[0];
            BGMSource.Stop();
        }

        public void BGMStart()
        {
            BGMSource.Play();
        }
        public void BGMStop()
        {
            BGMSource.Stop();
        }
        public void GameClearBGMPlay()
        {
            BGMSource.PlayOneShot(this.BGM[1]);
        }
    }
}