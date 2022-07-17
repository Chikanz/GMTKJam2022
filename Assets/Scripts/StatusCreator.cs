using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Makes status effects happen
public class StatusCreator : MonoBehaviour
{
    private List<Dave> daves = new List<Dave>();
    [SerializeField] private GameObject FireObj;

    public BodyManager BM;

    private float OGSpeed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //get all dave scripts in the scene
        GameObject[] daveObjs = GameObject.FindGameObjectsWithTag("Dave");
        
        //add them to the list
        foreach (GameObject dave in daveObjs)
            daves.Add(dave.GetComponent<Dave>());

        OGSpeed = daves[0].GetComponent<NavMeshAgent>().speed;

    }

    public enum eStatusEffect
    {
        // Drunk, //Worker is incapacited for a bit 
        Slow, //Worker is slowed for a bit
        Distracted, //Worker wonders off to a random point 
        Burn, //Something lights on fire
        Cringe, //All workers pause breifly to cringe
    }
    
    
    public void CreateStatus(eStatusEffect status)
    {
        //get a random dave
        Dave dave = daves[UnityEngine.Random.Range(0, daves.Count)];

        switch (status)
        {
            // case eStatusEffect.Drunk: //+ Tipsy
            //     MakeDrunk(dave);
            //     break;
            
            case eStatusEffect.Slow: //+ Tired
                StartCoroutine(Slow(OGSpeed/2, 5));
                break;
            
            case eStatusEffect.Distracted: //+ Boring story
                Distracted(dave);
                break;
            
            case eStatusEffect.Burn: //+ Heart burn
                SpawnFire();
                break;
            
            case eStatusEffect.Cringe: //CRINGE
                StartCoroutine(Slow(0, 2));
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(status), status, null);
        }
    }

    private void MakeDrunk(Dave d)
    {
        d.GetOnTheBeers();
    }

    private IEnumerator Slow(float slowSpeed, float slowTime)
    {
        foreach (Dave dave in daves)
        {
            dave.GetComponent<NavMeshAgent>().speed = slowSpeed;
        }
        yield return new WaitForSeconds(slowTime);
        
        foreach (Dave dave in daves)
        {
            dave.GetComponent<NavMeshAgent>().speed = OGSpeed;
        }
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
