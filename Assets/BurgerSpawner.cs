using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerSpawner : Station
{
    public GameObject borger;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void FixingStation(Dave d)
    {
            //Create another borger
            borger = Instantiate(borger, transform.position, Quaternion.identity) as GameObject;
            borger.transform.SetParent(transform);
    }
}