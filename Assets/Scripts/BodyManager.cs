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

    public int InterestPoints;
    public int InterestPointsMax = 100;
    public int StationBreakDamage = 10;
    
    public Slider InterestBarSlider;

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

        StartCoroutine(ChaosLoop());
    }

    IEnumerator ChaosLoop()
    {
        InterestPoints = InterestPointsMax;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(StationBreakTime.x, StationBreakTime.y));
            BreakStation();
        }
    }
    
    void Interest_Change(int delta)
    {
        if(InterestPoints + delta > InterestPointsMax)
        {
            InterestPoints = InterestPointsMax;
        }
        else if(InterestPoints + delta < 0)
        {
            InterestPoints = 0;
        }
        else
        {
            InterestPoints += delta;
        }
        
        InterestBarSlider.value = ( (float)InterestPoints / InterestPointsMax);
    }

    //Called when the station break event is triggered
    void Damage()
    {
        Interest_Change(-StationBreakDamage);
    }

    void BreakStation()
    {
        //Breaks station that's not already broken, after timer has elapsed
        List<Station> workingStations = new List<Station>();

        //filter station array for stations that are not broken
        for (int i = 0; i < Stations.Length; i++)
        {
            if (Stations[i].Status == Station.eStatus.Idle && !Stations[i].HasDave())
            {
                workingStations.Add(Stations[i]);
            }
        }

        //randomly select a station to break if there's a valid station to break
        if (workingStations.Count > 0)
        {
            var stationToBreak = workingStations[Random.Range(0, workingStations.Count)];
            stationToBreak.SetBroken();
        }
    }

    public void SpawnFire()
    {
        //Choose a random point on the navmesh
        
    }

    #endregion
}