using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JokeStation : Station
{
    public BodyManager BM;

    public int JokeInterestPoints = 20;

    public TextMeshPro JokeText;

    public string[] Jokes;
    
    // Start is called before the first frame update
    void Start()
    {
        JokeText.text = "";
    }

    protected override void FixingStation()
    {
        if (myDave.pickup && myDave.pickup.Type == Pickup.ePickupType.Energy)
        {
            Destroy(myDave.pickup.gameObject);
            myDave.SetPickup(null);
            
            BM.Interest_Change(JokeInterestPoints);
            
            //Pick a random joke from array
            int randomJoke = Random.Range(0, Jokes.Length);
            JokeText.text = Jokes[randomJoke];
            
            //set the joke text to clear after 5 seconds
            StopAllCoroutines();
            StartCoroutine(ClearJoke());
        }
    }

    IEnumerator ClearJoke()
    {
        yield return new WaitForSeconds(5);
        JokeText.text = "";
    }
}
