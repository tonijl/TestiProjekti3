using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject optionsPanel;
    
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

     void Start()
     {
        float master = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterSlider.value = master;
        musicSlider.value = music;
        sfxSlider.value = sfx;

        SetMasterVolume(master);
        SetMusicVolume(music);
        SetSFXVolume(sfx);
     }

     /*public void SaveMasterVolume(float volume)
     {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        SetMasterVolume(volume);

     }*/

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
         PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
         PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game exited!");
    }


}
