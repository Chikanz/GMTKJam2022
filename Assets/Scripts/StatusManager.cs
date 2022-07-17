using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Random timer for statuses
public class StatusManager : MonoBehaviour
{
    public Vector2 StatusTime = new Vector2(7,10);
    private StatusCreator SC;
    
    // Start is called before the first frame update
    void Start()
    {
        SC = GetComponent<StatusCreator>();
        StartCoroutine(ChooseRandomStatus());
    }

    IEnumerator ChooseRandomStatus()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(StatusTime.x, StatusTime.y));
            // SC.CreateStatus((StatusCreator.eStatusEffect)UnityEngine.Random.Range(0, Enum.GetValues(typeof(StatusCreator.eStatusEffect)).Length));
            SC.CreateStatus(StatusCreator.eStatusEffect.Burn);
            //todo show in 3D view
        }
    }
}
