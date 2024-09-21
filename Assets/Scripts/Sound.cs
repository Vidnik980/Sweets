using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    [SerializeField] private SwapItems[] sound;
    [SerializeField] private SwapItems[] music;
    [SerializeField] private AudioMixer audioMixer;
    private int volumeSound;
    private int volumeMusic;
    private void Start()
    {
        VolumeSound(PlayerPrefs.GetInt("Sound", 1));
        VolumeMusic(PlayerPrefs.GetInt("Music", 1));
    }
    private void SettingsVolume()
    {
        audioMixer.SetFloat("MyExposedParam", Mathf.Log10(volumeSound + 0.0001f) * 20);
        audioMixer.SetFloat("MyExposedParam1", Mathf.Log10(volumeMusic + 0.0001f) * 20);
    }
    public void VolumeSound(int volume)
    {
        if (volume == 2)
        {
            volumeSound = math.abs(volumeSound - 1);
        }
        else
        {
            volumeSound = volume;
        }
        foreach (SwapItems sound in sound)
        {
            sound.StateItems(volumeSound);
        }
        PlayerPrefs.SetInt("Sound", volumeSound);
        SettingsVolume();
    }
    public void VolumeMusic(int volume)
    {
        if (volume == 2)
        {
            volumeMusic = math.abs(volumeMusic - 1);
        }
        else
        {
            volumeMusic = volume;
        }
        foreach(SwapItems music in music)
        {
            music.StateItems(volumeMusic);
        }
        PlayerPrefs.SetInt("Music", volumeMusic);
        SettingsVolume();
    }
}
