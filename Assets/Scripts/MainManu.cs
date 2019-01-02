using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour {

    public GUISkin skin;

    private void OnGUI()
    {

        GUI.skin = skin;

        GUI.Label(new Rect(10, 10, 400, 75), "3D Puzzle Game");

        if (GUI.Button(new Rect(100, 100, 100, 45), "Play"))
        {
            SceneManager.LoadScene(1);
        }

        if (GUI.Button(new Rect(100, 150, 100, 45), "Quit"))
        {
            Application.Quit();
        }
    }
}
