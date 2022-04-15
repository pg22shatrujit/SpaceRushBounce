//Copyright (C) 2022 Shatrujit Aditya Kumar, All Rights Reserved
using UnityEngine;

public class BoundsTrigger : MonoBehaviour
{
    [SerializeField] PlaneMovement player;

    public static BoundsTrigger Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player exists the bounds, set isDead
        if(other.gameObject.name == Instance.player.gameObject.name)
        {
            other.gameObject.GetComponent<PlaneMovement>().IsDead = true;
        } 
        //Anything else, destroy it and boost the player's scroll
        else
        {
            Instance.player.ScoreUp();
            Destroy(other.gameObject);
        }
    }
}
