//Copyright (C) 2022 Shatrujit Aditya Kumar, All Rights Reserved
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyShip> m_spawnableShips;
    [SerializeField] BoundsTrigger m_bounds;
    [SerializeField] float m_xOffset = 5,
                           m_maxSpeed = 20,
                           m_spawnTimeMin = 0.5f,
                           m_spawnTimeMax = 2f;

    [SerializeField] public PlaneMovement m_target;

    float m_xPosition,
          m_yMinPosition,
          m_yMaxPosition;

    Quaternion m_spawnRotation;
    public static EnemySpawner Instance
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

    //Initialize starting values for position and rotation
    private void Start()
    {
        Instance.m_xPosition = Instance.m_bounds.transform.position.x + Instance.m_bounds.transform.localScale.x / 2 + m_xOffset;
        Instance.m_yMinPosition = Instance.m_bounds.transform.position.y - Instance.m_bounds.transform.localScale.y / 2;
        Instance.m_yMaxPosition = Instance.m_bounds.transform.position.y + Instance.m_bounds.transform.localScale.y / 2;

        Instance.m_spawnRotation = Quaternion.Euler(0, -90, 0);
        StartCoroutine(Spawn());
    }

    //Wait random time in seconds then spawn a ship
    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(GetRandomSpawnDelay());
            SpawnShip();
        }
    }

    //Wait for a random amount of time
    private float GetRandomSpawnDelay()
    {
        return Random.Range(Instance.m_spawnTimeMin, Instance.m_spawnTimeMax);
    }

    //Always spawn on the same XZ plane but with varying Y values, and rotate to face the player
    void SpawnShip()
    {
        int shipIndex = Random.Range(0, Instance.m_spawnableShips.Count);
        Vector3 startingPosition = new Vector3(Instance.m_xPosition, Random.Range(Instance.m_yMinPosition, Instance.m_yMaxPosition), 10);
        EnemyShip spawned = Instantiate(Instance.m_spawnableShips[shipIndex], startingPosition, Instance.m_spawnRotation);
        spawned.Target = Instance.m_target;
        spawned.GetComponent<Rigidbody>().velocity = new Vector3(-1 * m_maxSpeed, 0, 0);
    }
}
