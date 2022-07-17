using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum ePickupType
    {
        Burger,
        Energy,
    }

    public ePickupType Type;

    public bool PickedUp { get; private set; } = false;
    
    public void Grab()
    {
        PickedUp = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Set dave's pickup when it collides with the player
    private void OnTriggerEnter(Collider other)
    {
        if(!PickedUp && other.gameObject.CompareTag("Dave"))
        {
            var d = other.gameObject.GetComponent<Dave>();
            if (d.pickup == null)
            {
                Grab();
                d.SetPickup(this);
            }
        }
    }
}
