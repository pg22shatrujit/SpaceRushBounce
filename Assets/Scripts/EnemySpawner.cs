using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyShip> m_spawnableShips;
    [SerializeField] BoundsTrigger m_bounds;
    [SerializeField] float m_xOffset = 5;
    [SerializeField] float m_maxSpeed = 20;

    float m_xPosition,
          m_yMinPosition,
          m_yMaxPosition;

    private void Start()
    {
        m_xPosition = m_bounds.transform.position.x + m_bounds.transform.localScale.x / 2 + m_xOffset;
        m_yMinPosition = m_bounds.transform.position.y - m_bounds.transform.localScale.y / 2;
        m_yMaxPosition = m_bounds.transform.position.y + m_bounds.transform.localScale.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            SpawnShip();
    }

    void SpawnShip()
    {
        int shipIndex = Random.Range(0, m_spawnableShips.Count);
        Vector3 startingPosition = new Vector3(m_xPosition, Random.Range(m_yMinPosition, m_yMaxPosition), 0);
        EnemyShip spawned = Instantiate(m_spawnableShips[shipIndex], startingPosition, Quaternion.identity);
        spawned.GetComponent<Rigidbody>().velocity = new Vector3(-1 * m_maxSpeed, 0, 0);
    }
}
