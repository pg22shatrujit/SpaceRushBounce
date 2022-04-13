using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{

    [SerializeField] Rigidbody m_rigidbody;
    [SerializeField] float verticalForce = 20f,
                           horizontalSpeed = 20f;

    bool m_shouldJump,
         m_isDead;

    Vector3 m_jumpForce,
            m_horizontalVelocity;

    public bool IsDead
    {
        get { return m_isDead; }
        set { m_isDead = value; }
    }

    //public Vector3 HorizontalVelocity
    //{
    //    get { return m_horizontalVelocity; }
    //}

    private void Awake()
    {
        m_horizontalVelocity = new Vector3(horizontalSpeed, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_shouldJump = false;
        m_isDead = false;

        if (m_rigidbody == null)
            m_rigidbody = gameObject.GetComponent<Rigidbody>();

        m_jumpForce = new Vector3(0, verticalForce, 0);
        //m_rigidbody.velocity = m_horizontalVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            m_shouldJump = true;
        }
    }

    private void FixedUpdate()
    {
        if(m_shouldJump)
        {
            m_rigidbody.AddForce(m_jumpForce, ForceMode.VelocityChange);
            m_shouldJump = false;
        }
    }
}
