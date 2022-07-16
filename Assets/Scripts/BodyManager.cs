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
    Worker[] Workers;

    #endregion

    #region Methods
    
    void Awake()
    {

    }


    float Interest_Change(float amount)
    {
        var ib_cur = InterestBar_Cur; 

        //NEED CASE FOR <0
        return InterestBar_Cur = ib_cur > INTERESTBAR_MAX ? INTERESTBAR_MAX : ib_cur += amount;

    }

    void StationStatus_Change()
    {
        //Takes a Station[] arg called target

        /*
        -Set a timer for a random number of seconds
        -When timer is up change target's status to needs attention
            -Re-Execute every 1? second until the original timer is reset/changed

        -When timer !0 target's status is okay.
        */
    }

    void Damage()
    {
        //Call when damage event is triggered
    }

    #endregion
}

public class Station
{
    #region Variables

    #endregion
}

public class Worker
{
    #region Variables

    #endregion
}
