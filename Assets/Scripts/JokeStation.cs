using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokeStation : Station
{
    public BodyManager BM;

    public int JokeInterestPoints = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void FixingStation()
    {
        if (myDave.pickup && myDave.pickup.Type == Pickup.ePickupType.Energy)
        {
            Destroy(myDave.pickup.gameObject);
            myDave.SetPickup(null);
            
            BM.Interest_Change(JokeInterestPoints);
            Debug.Log("Interest changed!");
        }
    }
}
