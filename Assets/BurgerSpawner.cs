using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerSpawner : MonoBehaviour
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

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0)
        {
            //Create another borger
            borger = Instantiate(borger, transform.position, Quaternion.identity) as GameObject;
        }
    }
}
