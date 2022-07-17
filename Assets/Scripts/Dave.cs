using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

public class Dave : MonoBehaviour
{
    private NavMeshAgent agent;
    public Station myStation { get; private set; }
    public float DrunkTime = 5f;

    public Transform BurgerAnchor;
    private List<Material> materials = new List<Material>();
    private List<Color> DefaultCols = new List<Color>();

    public Animator AC;
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

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
        var MRS = GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < MRS.Length; i++)
        {
            for (int j = 0; j < MRS[i].materials.Length; j++)
            {
                materials.Add(MRS[i].materials[j]);
                DefaultCols.Add(MRS[i].materials[j].color);
            }
        }
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
        //Return all materials to default color
        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].color = DefaultCols[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        AC.SetBool(IsRunning, agent.velocity.sqrMagnitude > 0.1f);
    }

    public void highlight()
    {
        if (state == State.Drunk) return;
        foreach (var mat in materials)
        {
            mat.color = Color.red;
        }
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
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
