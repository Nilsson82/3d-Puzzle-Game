using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    // Count
    public float currentScore;
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
    public int winScreenWidth, winScreenHeight;

    private bool showWinScreen = false;

    public AudioClip[] audioClip;

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
                Time.timeScale = 1f;
            }
        }
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        PlaySound(1);
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
        stopTime = true;
        showWinScreen = true;

    }

    void LoadNextLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;

        if (currentLevel < 3)
        {
            currentLevel += 1;
            SaveGame();
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            stopTime = true;
            SceneManager.LoadScene(0);
        }
    }

    void SaveGame()
    {
        PlayerPrefs.SetInt("Level Completed", currentLevel);
        PlayerPrefs.SetFloat("Level " + currentLevel.ToString() + " score", currentScore);
    }

    void OnGUI()
    {
        GUI.skin = skin;
        

        if (startTime <= 5f)
        {
            //PlaySound(0);
            skin.GetStyle("Timer").normal.textColor = warningColorTimer;
        }
        else
        {
            skin.GetStyle("Timer").normal.textColor = defultColorTimer;
        }

        GUI.Label(timerRect, currentTime, skin.GetStyle("Timer"));
        GUI.Label(new Rect(45, 100, 200, 200), tokenCount.ToString() + "/" + totalTokenCount.ToString());

        if (GUI.Button(new Rect(Screen.width - 160, Screen.height - 50, 150, 40), "Back"))
        {
            SceneManager.LoadScene("main_menu");
            Time.timeScale = 1f;
        }

        if(showWinScreen)
        {
            Rect winScreenRect = new Rect(Screen.width / 2 - (Screen.width * .5f / 2), Screen.height / 2 - (Screen.height * .5f / 2), Screen.width * .5f, Screen.height * .5f);
            GUI.Box(winScreenRect, "You win!!!");

            if( GUI.Button(new Rect(winScreenRect.x + winScreenRect.width - 170, winScreenRect.y + winScreenRect.height - 60, 150, 40),  "Continue"))
            {
                LoadNextLevel();
            }

            if (GUI.Button(new Rect(winScreenRect.x + 20, winScreenRect.y + winScreenRect.height - 60, 150, 40), "Quit"))
            {
                SceneManager.LoadScene("main_menu");
                Time.timeScale = 1f;
            }

            currentScore = (float)tokenCount * startTime;
            GUI.Label(new Rect(winScreenRect.x + 20, winScreenRect.y + 40, 600, 50),  "Score: " + currentScore.ToString());
            GUI.Label(new Rect(winScreenRect.x + 20, winScreenRect.y + 90, 600, 150), "Completed Level: " + currentLevel);
        }

    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().clip = audioClip[clip];
        GetComponent<AudioSource>().Play();
    }


}
