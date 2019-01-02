using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour {

    public GUISkin skin;

    public AudioClip[] audioClip;

    private void OnGUI()
    {

        GUI.skin = skin;
        Rect winScreenRect = new Rect(Screen.width / 2 - (Screen.width * .5f / 2), Screen.height / 2 - (Screen.height * .5f / 2), Screen.width * .5f, Screen.height * .5f);
        GUI.Box(winScreenRect, "3D Puzzle Game");

        if (GUI.Button(new Rect(winScreenRect.x + winScreenRect.width / 2 - 150 / 2, winScreenRect.y + 45 * 2, 150, 40), "New Game"))
        {
            PlaySound(0);
            PlayerPrefs.SetInt("Level Completed", 1);
            SceneManager.LoadScene(1);
        }

        if (GUI.Button(new Rect(winScreenRect.x + winScreenRect.width / 2 - 150 / 2, winScreenRect.y  + 45 * 3, 150, 40), "Continue"))
        {
            PlaySound(0);
            if (GUI.Button(new Rect(100, 250, 100, 45), "Continue"))
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level Completed"));
            }
        }

        if (GUI.Button(new Rect(winScreenRect.x + winScreenRect.width/2 - 150/2, winScreenRect.y + 45 * 4, 150, 40), "Quit"))
        {
            PlaySound(0);
            Application.Quit();
        }

        GUI.skin = skin;
        GUI.Box(new Rect(winScreenRect.x + winScreenRect.width - 205, winScreenRect.y + winScreenRect.height - 25, 200, 20), "Music from Bensound.com");

    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }
}
