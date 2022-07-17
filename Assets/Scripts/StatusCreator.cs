using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes status effects happen
public class StatusCreator : MonoBehaviour
{
    private List<Dave> daves = new List<Dave>();
    [SerializeField] private GameObject FireObj;

    public BodyManager BM;
    
    // Start is called before the first frame update
    void Start()
    {
        //get all dave scripts in the scene
        GameObject[] daveObjs = GameObject.FindGameObjectsWithTag("Dave");
        
        //add them to the list
        foreach (GameObject dave in daveObjs)
            daves.Add(dave.GetComponent<Dave>());

    }

    public enum eStatusEffect
    {
        Drunk, //Worker is incapacited for a bit 
        Slow, //Worker is slowed for a bit
        Distracted, //Worker wonders off to a random point 
        Burn, //Something lights on fire
        // AllDrunk, //All workers start with Drunk
        // AllSlow, //All workers are slow during this date
        //
        // ///Outside Effects
        // Insult, //?
        // Awkward, //?
        // Cringe, //All workers pause breifly to cringe
        // Laugh, //?
        // Tired, //Stations take longer to fix for a bit
        //
        // ///Daytime effects
        // Stinky, //Lose a worker during this date (they're in shower)
        // ReallyTired, //All Stations take longer to fix during this date
        //
        // ///Personality effects
        // MoreTired, //Tired occurs more often
        // MoreBored, //Distracted occurs more often
        // MoreBurn, //Burn occurs more often
        // MoreBrain, //Brain station breaks more frequently
        // MoreLungs, //Lungs station breaks more frequently
        // MoreHeart, //Heart station breaks more frequently
        // MorePiss, //Bladder? station breaks more frequently
        // MoreHands //Hands station breaks more frequently
    }
    
    
    public void CreateStatus(eStatusEffect status)
    {
        //get a random dave
        Dave dave = daves[UnityEngine.Random.Range(0, daves.Count)];

        switch (status)
        {
            case eStatusEffect.Drunk:
                MakeDrunk(dave);
                break;
            case eStatusEffect.Slow:
                break;
            case eStatusEffect.Distracted:
                Distracted(dave);
                break;
            case eStatusEffect.Burn:
                SpawnFire();
                break;
            // case eStatusEffect.AllDrunk:
            //     break;
            // case eStatusEffect.AllSlow:
            //     break;
            // case eStatusEffect.Insult:
            //     break;
            // case eStatusEffect.Awkward:
            //     break;
            // case eStatusEffect.Cringe:
            //     break;
            // case eStatusEffect.Laugh:
            //     break;
            // case eStatusEffect.Tired:
            //     break;
            // case eStatusEffect.Stinky:
            //     break;
            // case eStatusEffect.ReallyTired:
            //     break;
            // case eStatusEffect.MoreTired:
            //     break;
            // case eStatusEffect.MoreBored:
            //     break;
            // case eStatusEffect.MoreBurn:
            //     break;
            // case eStatusEffect.MoreBrain:
            //     break;
            // case eStatusEffect.MoreLungs:
            //     break;
            // case eStatusEffect.MoreHeart:
            //     break;
            // case eStatusEffect.MorePiss:
            //     break;
            // case eStatusEffect.MoreHands:
            //     break;
            default:
                throw new ArgumentOutOfRangeException(nameof(status), status, null);
        }
    }

    private void MakeDrunk(Dave d)
    {
        d.GetOnTheBeers();
    }

    private void Distracted(Dave d)
    {
        d.myStation.AssignDave(null);
        d.SetStation(null);
        
        d.SetDestination(Util.GetRandomNavmeshPoint());
    }

    private void SpawnFire()
    {
        //Choose a random point on the navmesh
        Vector3 randomPoint = Util.GetRandomNavmeshPoint();

        //Spawn a fire at that point
        GameObject fire = Instantiate(FireObj, randomPoint, Quaternion.identity);
        var station = fire.GetComponent<Station>();
        station.SetBroken();
        station.OnDamage += BM.Damage;
        station.OnFixed += () =>
        {
            station.OnDamage -= BM.Damage;
            Destroy(fire);
        };
    }

    void AllDrunk()
    {

    }

    void AlllSlow()
    {

    }

    void Insult() { }

    void Awkaward() { }

    void Cringe() { }

    void Laugh() { }

    void Tired() { }

    void Stinky() { }

    void ReallyTired() { }

    void MoreEffect() { }
    void MoreArea() { }
}
