using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dave : MonoBehaviour
{
    private NavMeshAgent agent;
    public Station myStation { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 position)
    {
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
}
