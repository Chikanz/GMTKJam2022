using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    ///Summary
    ///Runs the game in its correct sequence
    ///-Shows Profile UI
    ///    --OnClick move to next step
    ///-Enable RoundStart and the RoundTimer
    ///-Disable RoundStart & RoundTimer when...
    ///    --The Love-o-meter drops to 0
    ///        ---Display GameOver UI
    ///    --RoundTimer Reaches 0
    ///        ---Display the round transition UI & disable it when it's animation finishes
    ///        Also reset the play part of the scene.

    public static ManagerScript current;

    //public bool roundStart = false;
    [SerializeField] GameObject masterUI;

    private void Awake()
    {
        current = this;
    }

    [SerializeField] UnityEvent OnRoundStart;
    [SerializeField]UnityEvent OnTimerEnd; //Display Transition UI & Hide Timer
    [SerializeField]UnityEvent OnMeterEmpty; //Display Gameover UI & Hide Timer
    [SerializeField]UnityEvent OnTransitionEnd;//Hide Transition UI, Display Timer, & Reset all non UI objects in scene

    public void TimerEnd()
    {
        OnTimerEnd.Invoke();
        //Play Transition
        //
        
    }

    public void MeterEmpty()
    {
        OnMeterEmpty.Invoke();
    }

    public void RoundStart()
    {
        OnRoundStart.Invoke();
    }

    public void TransitionEnd()
    {
        OnTransitionEnd.Invoke();
 
    }

    public void LoadLevel()
    {
        DontDestroyOnLoad(masterUI);
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("WorkerTest");
    }



}
