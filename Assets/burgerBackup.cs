using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgerBackup : MonoBehaviour
{
    public bool isEnergy = false;
    public GameObject PickupObj;
    
    //If worker enters trigger give them a burger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dave"))
        {
            var myDave = other.GetComponent<Dave>();
            if ((myDave.pickup && myDave.pickup.Type == Pickup.ePickupType.Burger) || (!isEnergy && !myDave.pickup))
            {
                if(myDave.pickup) Destroy(myDave.pickup.gameObject);
                var energyObj = Instantiate(PickupObj, myDave.transform.position, Quaternion.identity);

                var energy = energyObj.GetComponent<Pickup>();
                energy.Grab();
                myDave.SetPickup(energy);
                energyObj.transform.localPosition = Vector3.zero;
            }
        }
    }
}