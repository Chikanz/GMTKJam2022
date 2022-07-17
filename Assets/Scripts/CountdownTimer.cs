using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    ///SUmmary
    ///Read the round lendth from DateManager
    ///Check the round has started, and if so start counting down to 0 from the pulled round length
    ///When the timer is up, trigger the 'Round End' event
    #region Variables
    float timer = 0f;
    int timerLength;
    public bool roundStart = false;
    //[SerializeField]DateManager date;
    TextMeshProUGUI displayText;
    [SerializeField] ManagerScript manager;
    #endregion

    private void Awake()
    {
        displayText = GetComponent<TextMeshProUGUI>();

    }

    public void OnEnable()
    {
        timerLength = 30;//date.dateLength;
        timer = timerLength;
        roundStart = true;
    }

    private void Update()
    {
        if(roundStart) //Probs gonna break something or be dumb later
        {
            Countdown();
        }
        else
        {
            timer = timerLength;
        }

        DisplayTimer();
    }

    void Countdown()
    {
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            manager.TimerEnd();
        }
    }

    void DisplayTimer()
    {
        displayText.text = timer.ToString("F0");
    }
}
