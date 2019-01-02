using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour {

    public GUISkin skin;

    private void OnGUI()
    {

        GUI.skin = skin;

        GUI.Label(new Rect(100, 100, 400, 75), "3D Puzzle Game");

        if (GUI.Button(new Rect(100, 200, 100, 45), "New Game"))
        {
            PlayerPrefs.SetInt("Level Completed", 1);
            SceneManager.LoadScene(1);
        }

        if (PlayerPrefs.GetInt("Level Completed") > 1)
        {
            if (GUI.Button(new Rect(100, 250, 100, 45), "Continue"))
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("Level Completed"));
            }
        }

        if (GUI.Button(new Rect(100, 300, 100, 45), "Quit"))
        {
            Application.Quit();
        }
    }
}
