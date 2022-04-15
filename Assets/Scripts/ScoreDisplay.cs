//Copyright (C) 2022 Shatrujit Aditya Kumar, All Rights Reserved
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_scoreText,
                                     m_highScoreText;

    public static ScoreDisplay Instance
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

    //Set scores on scene load
    void Start()
    {
        Instance.m_scoreText.text = "Score: " + PlayerPrefs.GetInt("Score");
        Instance.m_highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }

    //Load a different scene, determined by the buttons
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
