using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour {

    public AudioClip[] audioClip;

    public void PlayGame()
    {
        PlaySound(0);
        PlayerPrefs.SetInt("Level Completed", 1);
        SceneManager.LoadScene("World select");
    }

    public void Continue()
    {
        PlaySound(0);
        SceneManager.LoadScene("World select");
    }

    public void Options()
    {
        PlaySound(0);
    }

    public void Quit()
    {
        PlaySound(0);
        Application.Quit();
    }

void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
        GetComponent<AudioSource>().Play();
    }
}
