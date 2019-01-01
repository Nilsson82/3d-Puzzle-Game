using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static int currentScore;
    public static int highscore;

    public static int currentLevel;
    public static int unlockedLevel;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void CompleteLevel()
    {
        if(currentLevel < 2)
        {
            currentLevel += 1;
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            print("You win!!!");
        }


    }
    

}
