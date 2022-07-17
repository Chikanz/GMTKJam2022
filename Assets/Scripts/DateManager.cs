using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour
{
    /// Summary
    /// -Track & store active Date Traits, and Daytime Effects
    /// -Track how long is left on the date
    /// -Generate new date info


    #region Variables
    public int dateLength;
    [SerializeField] float timeLeft;
    DateProfile dateInfo;
    [SerializeField]Profession[] jobsList;
    #endregion

    private void Start()
    {
        FillJobsList();
    }

    void GenerateDate()
    {
        ///Create DateProfile object
        dateInfo = new DateProfile(PickName(), PickAge(), PickJob());
        
    }

    string PickName()
    {
        return "i'm unpossible";
    }

    int PickAge()
    {
        return Random.Range(18,102);
    }

    Profession PickJob()
    {
        return jobsList[Random.Range(0,jobsList.Length)];
    }

    void FillJobsList()
    {
        jobsList[0] = new Profession(StatusCreator.eStatusEffect.Slow,"Personal Trainer");
        jobsList[1] = new Profession(StatusCreator.eStatusEffect.Drunk, "DJ");
        // jobsList[2] = new Profession(StatusCreator.eStatusEffect.MoreTired, "Dancer");
        // jobsList[3] = new Profession(StatusCreator.eStatusEffect.MoreBrain, "Therapist");
        // jobsList[4] = new Profession(StatusCreator.eStatusEffect.MorePiss, "Chef");
        // jobsList[5] = new Profession(StatusCreator.eStatusEffect.MoreBored, "Scientist");

    }

}

struct DateProfile
{
    public string Name;
    public int Age;
    public Profession Job;

    //Constructor
    public DateProfile(string name, int age, Profession job)
    {
        this.Name = name;
        this.Age = age;
        this.Job = job;
    }

}

struct Profession
{
    public StatusCreator.eStatusEffect Effect;
    public string Name;

    public Profession(StatusCreator.eStatusEffect effect, string jobName)
    {
        this.Effect = effect;
        this.Name = jobName;
    }
}
