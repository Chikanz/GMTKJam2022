using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    #region Variables
    float InterestBar_Cur;
    const int INTERESTBAR_MAX = 100;
    const int INTERESTBAR_MIN = 0;

    Station[] Stations;
    Dave[] Workers;

    #endregion

    #region Methods

    void Awake()
    {
        for (int i = 0; i < Stations.Length; i++)
        {
            Stations[i-1].OnDamage += Damage;
        }
        
    }


    float Interest_Change(float amount)
    {
        var ib_cur = InterestBar_Cur;

        //NEED CASE FOR <0
        return InterestBar_Cur = ib_cur > INTERESTBAR_MAX ? INTERESTBAR_MAX : ib_cur += amount;

    }

    void Damage()
    {

        //Call when the station break event is triggered
        InterestBar_Cur -= 5f; //Need to determine the damage per tick
    }

    void BreakStation()
    {
        //Breaks station that's not already broken, after timer has elapsed
        List<Station> workingStations = new List<Station>();
        //filter station array for stations that are not broken
        for(int i = 0; i < Stations.Length; i++)
        {
            if(Stations[i].Status == Station.eStatus.Idle)
            {
                workingStations.Add(Stations[i]);
            }
        }
        //randomly select a station to break
        var stationToBreak = workingStations[Random.Range(0, workingStations.Count)];
        stationToBreak.SetBroken();



    }

    #endregion
}

