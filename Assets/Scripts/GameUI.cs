//Copyright (C) 2022 Shatrujit Aditya Kumar, All Rights Reserved
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeReference] private PlaneMovement m_player;
    [SerializeField] private Image m_healthBarFill;
    [SerializeField] private TextMeshProUGUI m_scoreText;
    public static GameUI Instance
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

    // Update fill for the health bar and current score
    void Update()
    {
        Instance.m_healthBarFill.fillAmount = Instance.m_player != null ? Instance.m_player.HealthPercent : 0f;
        Instance.m_scoreText.text = Instance.m_player.Score.ToString();
    }
}
