using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    // Count
    public int currentScore;
    public int highscore;
    public int tokenCount;
    public int currentLevel;
    public int unlockedLevel;
    private int totalTokenCount;

    // GUI Skin
    public GUISkin skin;

    // Timer variables
    public Rect timerRect;
    public Color warningColorTimer;
    public Color defultColorTimer;
    private bool stopTime;
    public float startTime;
    private string currentTime;

    // References
    public GameObject tokenParent;

    private void Update()
    {
        if (!stopTime)
        {
            startTime -= Time.deltaTime;
            currentTime = string.Format("{0:0.000}", startTime);

            if (startTime <= 0)
            {
                startTime = 0;
                currentTime = "You're out of time";
                SceneManager.LoadScene(0);
            }
        }
    }

    private void Start()
    {
        if (tokenParent != null)
        {
            totalTokenCount = tokenParent.transform.childCount;
        }
        if (PlayerPrefs.GetInt("Level Completed") > 0)
        {
            currentLevel = PlayerPrefs.GetInt("Level Completed");
        }
        else
        {
            currentLevel = 1;
        }
        stopTime = false;
    }

    public void CompleteLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel < 3)
        {
            currentLevel += 1;
            PlayerPrefs.SetInt("Level Completed", currentLevel);
            PlayerPrefs.SetInt("Level " + currentLevel.ToString() + " score", currentScore);

            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            stopTime = true;
            currentTime = "You win!!!";
            SceneManager.LoadScene(0);
        }

    }

    public void AddToken()
    {
        tokenCount += 1;

    }

    void OnGUI()
    {
        GUI.skin = skin;

        if(startTime <= 5f)
        {
            skin.GetStyle("Timer").normal.textColor = warningColorTimer;
        }
        else
        {
            skin.GetStyle("Timer").normal.textColor = defultColorTimer;
        }

        GUI.Label(timerRect, currentTime, skin.GetStyle("Timer"));
        GUI.Label(new Rect(45, 100, 200, 200), tokenCount.ToString() + "/" + totalTokenCount.ToString());

        if (GUI.Button(new Rect(10, 100, 100, 45), "Back"))
        {
            SceneManager.LoadScene(0);
        }

    }


}
