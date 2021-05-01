using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControlManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGMVol", volume);
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SEVol", volume);
    }
}
