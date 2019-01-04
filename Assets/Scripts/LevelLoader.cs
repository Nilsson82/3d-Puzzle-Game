using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class LevelLoader : MonoBehaviour {
    public int levelToLoad;
    public GameObject padlock;
    private string loadPrompt;
    private int completedLevel;
    private bool inRange;
    private bool canLoadLevel;


    private void Start()
    {
        completedLevel = PlayerPrefs.GetInt("Level Completed");
        if (completedLevel < 1)
        {
            completedLevel = 1;
        }
        canLoadLevel = levelToLoad <= completedLevel ? true : false;

        if ( !canLoadLevel)
        {
            Instantiate(padlock, new Vector3(transform.position.x, 0.5f, transform.position.z - 2f), Quaternion.Euler(0, 180, 0));
        }

    }

    private void Update()
    {
        if (canLoadLevel && CrossPlatformInputManager.GetButtonDown("Action") && inRange)
        //if (canLoadLevel && Input.GetButtonDown("Action") && inRange)
        {
            SceneManager.LoadScene("Level" + levelToLoad.ToString());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        inRange = true;
        if (canLoadLevel)
        {
#if UNITY_ANDROID
            loadPrompt = "[Action] to load level " + levelToLoad.ToString();
#else
            loadPrompt = "[E] to load level " + levelToLoad.ToString();
#endif
        }
        else
        {
            loadPrompt = "Level " + levelToLoad.ToString() + " is locked";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        loadPrompt = "";
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(30, Screen.height * .9f, 200, 40), loadPrompt);
    }

}
