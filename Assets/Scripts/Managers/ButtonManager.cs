using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Managers
{
    public class ButtonManager : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private AudioClip SE;
        private AudioSource SESource;

        void Start()
        {
            SESource = this.GetComponent<AudioSource>();
            SESource.clip = SE;
        }

        public void MoveSelectScene()
        {
            SceneManager.LoadScene("Select Scene");
        }

        public void MoveBonusScene()
        {
            SceneManager.LoadScene("Bonus Scene");
        }

        public void MoveInformationScene()
        {
            SceneManager.LoadScene("Information Scene");
        }

        public void MoveSettingScene()
        {
            SceneManager.LoadScene("Setting Scene");
        }

        public void MoveStage1gScene()
        {
            SceneManager.LoadScene("Stage1 Scene");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointEnter");
            SESource.Play();
        }
    }
}