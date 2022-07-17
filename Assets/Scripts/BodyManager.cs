using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyManager : MonoBehaviour
{
    #region Variables

    private Station[] Stations;
    private Dave[] Workers;

    public Vector2 StationBreakTime;

    public int InterestPoints { get; private set; }
    public int InterestPointsMax = 100;

    public Slider InterestBarSlider;

    public int maxBroken = 5;
    [SerializeField] ManagerScript manager;

    #endregion

    #region Methods

    void Awake()
    {
        Stations = GetComponentsInChildren<Station>();

        for (int i = 0; i < Stations.Length; i++)
        {
            Stations[i].OnDamage += Damage;
        }

        InterestBarSlider.value = 1;
        InterestPoints = InterestPointsMax;
        
        // StartCoroutine(ChaosLoop());
        // StartCoroutine(InterestDecay());
    }

    IEnumerator ChaosLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(StationBreakTime.x, StationBreakTime.y));
            BreakStation();
        }
    }
    
    IEnumerator InterestDecay()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Interest_Change(-1);
        }
    }

    public void Interest_Change(int delta)
    {
        if (InterestPoints + delta > InterestPointsMax)
        {
            InterestPoints = InterestPointsMax;
        }
        else if (InterestPoints + delta < 0)
        {
            InterestPoints = 0;
        }
        else
        {
            InterestPoints += delta;
        }

        InterestBarSlider.value = ((float)InterestPoints / InterestPointsMax);

        if(InterestBarSlider.value <= 0f)
        {
            manager.MeterEmpty();
        }
    }

    //Called when the station break event is triggered
    public void Damage(int damage)
    {
        Interest_Change(-damage);
    }

    void BreakStation()
    {
        //Breaks station that's not already broken, after timer has elapsed
        List<Station> workingStations = new List<Station>();
        
        //Count how many staions are broken + filter station array for stations that are not broken
        int brokenStations = 0;
        for (int i = 0; i < Stations.Length; i++)
        {
            if (Stations[i].Status == Station.eStatus.Broken)
            {
                brokenStations++;
            }
            else if(!Stations[i].isStationed) //dont select ones already stationed
            {
                workingStations.Add(Stations[i]);
            }
        }
        
        //Don't break any more if already at max
        if(brokenStations >= maxBroken) return; 

        //randomly select a station to break if there's a valid station to break
        if (workingStations.Count > 0)
        {
            var stationToBreak = workingStations[Random.Range(0, workingStations.Count)];
            stationToBreak.SetBroken();
        }
    }
}

#endregion
