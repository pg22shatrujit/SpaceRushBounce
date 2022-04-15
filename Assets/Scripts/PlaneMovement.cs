//Copyright (C) 2022 Shatrujit Aditya Kumar, All Rights Reserved
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneMovement : MonoBehaviour
{

    [SerializeField] Rigidbody m_rigidbody;
    [SerializeField] MeshRenderer m_renderer;
    [SerializeField] float m_verticalForce = 20f,
                           m_currentHealth = 10f,
                           m_maxHealth = 10f;

    bool m_shouldJump,
         m_isDead,
         m_isHit;

    float m_blinkTime;

    Vector3 m_jumpForce;

    int m_score;

    public static PlaneMovement Instance
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
    
    public bool IsDead
    {
        get { return Instance.m_isDead; }
        set { Instance.m_isDead = value; }
    }

    public float HealthPercent
    {
        get { return Instance.m_currentHealth / Instance.m_maxHealth; }
    }

    public int Score
    {
        get { return Instance.m_score; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance.m_shouldJump = false;
        Instance.m_isDead = false;
        Instance.m_blinkTime = 1.0f;
        Instance.m_score = 0;

        //Initialize rigidbody and renderer if their null
        if (Instance.m_rigidbody == null)
            Instance.m_rigidbody = gameObject.GetComponent<Rigidbody>();

        if (Instance.m_renderer == null)
        {
            Instance.m_renderer = gameObject.GetComponent<MeshRenderer>();
        }

        Instance.m_jumpForce = new Vector3(0, Instance.m_verticalForce, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Queue's a jump for fixed update
        if(Input.GetButtonDown("Jump")) {
            Instance.m_shouldJump = true;
        }

        //If we're dead, set score and high score if valid and go to the next scene
        if(IsDead)
        {
            int highScore = 0;

            if(PlayerPrefs.HasKey("HighScore"))
            {
                highScore = PlayerPrefs.GetInt("HighScore");
            }

            PlayerPrefs.SetInt("Score", Instance.m_score);

            if (Instance.m_score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", Instance.m_score);
            }
            SceneManager.LoadScene("EndScreen");
        }
    }

    //Check if we're invulnerable, take damage if not. Then blink if not dead
    public void GetHit()
    {
        if(!Instance.m_isHit)
        {
            TakeDamage();
            if(!Instance.m_isDead) StartCoroutine(Blink(Instance.m_blinkTime));
        }
    }

    //Drop our health
    void TakeDamage()
    {
        if(--Instance.m_currentHealth <= 0)
        {
            Instance.m_isDead = true;
        }
    }

    public void ScoreUp()
    {
        Instance.m_score++;
    }

    //Mimic blinking by turning the renderer on and off
    IEnumerator Blink(float time)
    {
        Instance.m_isHit = true;
        float endTime = Time.time + time;
        while(Time.time < endTime)
        {
            Instance.m_renderer.enabled = false;
            yield return new WaitForSeconds(0.05f);
            Instance.m_renderer.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
         Instance.m_isHit = false;
    }

    //Add force if jump is queued
    private void FixedUpdate()
    {
        if(Instance.m_shouldJump)
        {
            Instance.m_rigidbody.AddForce(m_jumpForce, ForceMode.VelocityChange);
            Instance.m_shouldJump = false;
        }
    }
}
