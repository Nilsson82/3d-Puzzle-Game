using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public  int currentScore;
    public static int highscore;

    public static int currentLevel;
    public static int unlockedLevel;

    public Rect timerRect;
    public Color warningColorTimer;
    public Color defultColorTimer;


    public GUISkin skin;

    private static bool stopTime;
    public float startTime;
    private static string currentTime;


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
        stopTime = false;
        DontDestroyOnLoad(gameObject);
    }

    public static void CompleteLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel < 3)
        {
            currentLevel += 1;
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            stopTime = true;
            currentTime = "You win!!!";
            SceneManager.LoadScene(0);
        }

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

    }


}
