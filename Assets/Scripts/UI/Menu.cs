using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
 
    public void PausePress()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        PauseMenu?.SetActive(false);
        Time.timeScale = 1f;
    }
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Ê¹ÓÃÍË³ö");
    }
}
