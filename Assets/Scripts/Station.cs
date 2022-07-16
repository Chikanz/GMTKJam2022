using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public int TimeToDamage = 10;
    private float damageTimer = 0;

    //how long it takes to fix this sation
    public int TimeToFix = 3;
    private float progress;
    
    public Slider progressBar; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //call this when the station needs to be fucked up
    [ContextMenu("Set broken")]
    public void SetBroken()
    {
        Status = eStatus.Broken;
        damageTimer = TimeToDamage;
        progress = 0;
        GetComponentInChildren<Light>().enabled = true;
        Debug.Log("Station broken");
    }
    
    public void FixStation()
    {
        Status = eStatus.Idle;
        progress = 0;
        GetComponentInChildren<Light>().enabled = false;
        Debug.Log("Station fixed");
    }

    public bool HasDave()
    {
        return myDave != null && Vector3.Distance(myDave.transform.position, davePoint.position) < 1;
    }

    //Make a dave come to this station
    public void AssignDave(Dave d)
    {
        //remove the selected dave from it's station
        if (d && d.myStation != null)
        {
            d.myStation.AssignDave(null);
        }

        // if(myDave) return; //station already has a dave (doesn't work lmao)
        
        myDave = d;

        if (d)
        {
            d.SetDestination(davePoint.position);
            d.SetStation(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Keep track of how long the station has been broken for
        if (Status == eStatus.Broken && progress == 0) 
        {
            if(damageTimer > 0) damageTimer -= Time.deltaTime;
            else if (damageTimer < 0)
            {
                OnDamage?.Invoke();
                Debug.Log("Station damaged!");
                damageTimer = TimeToDamage;
            }
        }
        
        //Check if dave is fixing the station by checking distance
        if (myDave != null)
        {
            if (Vector3.Distance(myDave.transform.position, davePoint.position) < 1)
            {
                progress += Time.deltaTime;
            }
        }
        
        //Make the station fixed 
        if(Status == eStatus.Broken && progress > TimeToFix)
        {
            FixStation();
        }
        
        //Update the progress bar
        progressBar.gameObject.SetActive(Status == eStatus.Broken && progress > 0);
        if(Status == eStatus.Broken && progress > 0)
        {
            progressBar.value = progress / TimeToFix;
        }

    }
}
