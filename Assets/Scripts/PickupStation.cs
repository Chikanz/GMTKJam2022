using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PickupStation : Station
{
    public bool energyStation = false;
    
    [FormerlySerializedAs("EnergyObject")] public GameObject PickupObj;
    protected override void FixingStation(Dave d)
    {
        var myDave = d;
        if ((myDave.pickup && myDave.pickup.Type == Pickup.ePickupType.Burger) || (!energyStation && !myDave.pickup))
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
