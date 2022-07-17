using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TummyStation : Station
{
    public GameObject EnergyObject;
    protected override void FixingStation()
    {
        if (myDave.pickup && myDave.pickup.Type == Pickup.ePickupType.Burger)
        {
            Destroy(myDave.pickup.gameObject);
            var energyObj = Instantiate(EnergyObject, myDave.transform.position, Quaternion.identity);

            var energy = energyObj.GetComponent<Pickup>();
            energy.Grab();
            myDave.SetPickup(energy);
            energyObj.transform.localPosition = Vector3.zero;
        }
    }
}
