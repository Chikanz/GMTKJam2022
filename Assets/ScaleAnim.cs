using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnim : MonoBehaviour
{
    public float speed = 1;
    public AnimationCurve scaleCurve;
    public float magnitude = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, scaleCurve.Evaluate((Time.time * speed) % 1) * magnitude, 1);
    }
}
