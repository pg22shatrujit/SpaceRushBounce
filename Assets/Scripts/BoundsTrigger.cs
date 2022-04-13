using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "PlayerPlane")
        {
            other.gameObject.GetComponent<PlaneMovement>().IsDead = true;
        } else
        {
            Destroy(other.gameObject);
        }
    }
}
