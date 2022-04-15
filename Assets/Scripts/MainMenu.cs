//Copyright (C) 2022 Shatrujit Aditya Kumar, All Rights Reserved
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_highScoreText;
    public static MainMenu Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    //Set Highscore on start
    void Start()
    {
        SetHighScore();
    }

    //Set the highscore
    void SetHighScore()
    {
        if (!PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetInt("HighScore", 0);
        Instance.m_highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    //Goes to the game scene
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    //Exit the app
    public void Quit()
    {
        Debug.Log("Application should quit.");
        Application.Quit();
    }

    //Reset high score to zero
    public void ResetScores()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        SetHighScore();
    }
}
