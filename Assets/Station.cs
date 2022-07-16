using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public enum eStatus
    {
        Idle,
        Broken,
    } 
    
    public eStatus Status { get; private set; }
    
    public Transform davePoint;
    private Dave myDave;
    
    public delegate void DamageDelegate();
    public DamageDelegate OnDamage; //Called when station elapses time to fix

    //how much time the station can be broken before damage occurs
    public int TimeToFix = 10;
    private float progress;
    private float damageTimer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //call this when the station needs to be fucked up
    public void SetBroken()
    {
        Status = eStatus.Broken;
        //...
    }

    //Make a dave come to this station
    public void AssignDave(Dave d)
    {
        myDave = d;
        d.SetDestination(davePoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
