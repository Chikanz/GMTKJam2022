using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dave : MonoBehaviour
{
    private NavMeshAgent agent;
    public Station myStation { get; private set; }
    public float DrunkTime = 5f;

    public Transform BurgerAnchor;
    
    public enum State
    {
       Working,
       Drunk,
    }
    
    public Pickup pickup { get; private set; }
    
    public State state {get ; private set;} = State.Working;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 position)
    {
        if(state == State.Drunk) return; // if drunk, don't move
        
        if(myStation) myStation.AssignDave(null);
        agent.SetDestination(position);
        UnHighlight();
    }

    public void SetStation(Station station)
    {
        myStation = station;
    }

    public void UnHighlight()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void highlight()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    //Make this dave lit af
    public void GetOnTheBeers()
    {
        state = State.Drunk;
        if(myStation) myStation.AssignDave(null);
        agent.SetDestination(transform.position);
        //play drunk anim
        Invoke(nameof(GetOffTheBeers), DrunkTime);
    }
    
    public void SetPickup(Pickup pickup)
    {
        this.pickup = pickup;
        if(!pickup) return;
        
        pickup.transform.SetParent(BurgerAnchor, false);
        pickup.transform.localPosition = Vector3.zero;
        pickup.transform.localScale = Vector3.one;
    }
    
    public void GetOffTheBeers()
    {
        state = State.Working;
    }
}
