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
}
