using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private static Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        //get camera reference
        if(!cam) cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //always make the  axis look at the camera
        transform.LookAt(cam.transform);
    }
}
