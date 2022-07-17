using System;
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
    protected Dave myDave;

    public delegate void DamageDelegate(int damage);

    public DamageDelegate OnDamage; //Called when station elapses time to fix

    public delegate void RepairDelegate();

    public RepairDelegate OnFixed; //Called when station is fixed

    //how much time the station can be broken before damage occurs
    public int TimeToDamage = 10;
    private float damageTimer = 0;

    //how long it takes to fix this sation
    public int TimeToFix = 3;
    private float progress;

    public Slider progressBar;

    public GameObject AlertObj;
    private Light light;

    [Tooltip("The amount of damage this does when broken")] [SerializeField]
    private int InterestDamage = 10;

    public AnimationCurve LightPulseCurve;
    [SerializeField] private float distanceToFix = 1.5f;

    private List<Dave> daves = new List<Dave>();

    public bool isStationed { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        light = GetComponentInChildren<Light>();

        //Get all daves in the scene
        if (daves.Count == 0)
        {
            var daveObjs = GameObject.FindGameObjectsWithTag("Dave");
            foreach (var obj in daveObjs)
            {
                var dave = obj.GetComponent<Dave>();
                if (dave != null)
                {
                    daves.Add(dave);
                }
            }
        }
        
        if(daves.Count == 0) Debug.LogError("SHIT IS FUCKED!!!");
    }

    private void OnDestroy()
    {
        if(daves.Count > 0) daves.Clear();
    }

    //call this when the station needs to be fucked up
    [ContextMenu("Set broken")]
    public void SetBroken()
    {
        Status = eStatus.Broken;
        damageTimer = TimeToDamage;
        progress = 0;
        if (light) light.enabled = true;
        if (AlertObj) AlertObj.SetActive(true);

        Debug.Log("Station broken");
    }


    public void FixStation()
    {
        Status = eStatus.Idle;
        progress = 0;
        if (light) light.enabled = false;
        if (AlertObj) AlertObj.SetActive(false);
        OnFixed?.Invoke();
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
        isStationed = false;

        //Keep track of how long the station has been broken for
        if (Status == eStatus.Broken && progress == 0)
        {
            if (damageTimer > 0) damageTimer -= Time.deltaTime;
            else if (damageTimer < 0)
            {
                OnDamage?.Invoke(InterestDamage);
                Debug.Log("Station damaged!");
                damageTimer = TimeToDamage;
            }
        }

        //Check if dave is fixing the station by checking distance
        for (int i = 0; i < daves.Count; i++)
        {
            var dave = daves[i];
            {
                var dist = Vector3.Distance(dave.transform.position, davePoint.position);
                if (dist <= distanceToFix)
                {
                    isStationed = true;
                    FixingStation(dave);
                }
            }
        }

        //Make the station fixed 
        if (Status == eStatus.Broken && progress > TimeToFix)
        {
            FixStation();
        }

        //Update the progress bar
        if (progressBar)
            progressBar.gameObject.SetActive(Status == eStatus.Broken && progress > 0);
        if (Status == eStatus.Broken && progress > 0)
        {
            progressBar.value = progress / TimeToFix;
        }

        //Pulse light
        if (light && light.enabled) light.intensity = LightPulseCurve.Evaluate(Time.time % 1) * 5;
    }

    //called when the dave is close to station
    protected virtual void FixingStation(Dave d)
    {
        progress += Time.deltaTime;
    }
}