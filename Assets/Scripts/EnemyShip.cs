//Copyright (C) 2022 Shatrujit Aditya Kumar, All Rights Reserved
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    PlaneMovement m_target;

    public PlaneMovement Target
    {
        get { return m_target; }
        set { m_target = value; }
    }

    //Check if we hit the player and reduce their health
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == m_target.name)
         {
            m_target.GetHit();
        }
    }
}
