using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaveSelector : MonoBehaviour
{
    private Dave selectedDave;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get the world position of screen click on the attached camera
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //If we raycast a dave and don't have one already selected
                if (!selectedDave && hit.transform.TryGetComponent(out Dave d))
                {
                    selectedDave = d;
                    d.highlight();
                }
                //If we already have a selected dave move them to the new position
                else if (selectedDave && hit.transform.TryGetComponent(out Station station)) 
                {
                    station.AssignDave(selectedDave);
                    selectedDave.UnHighlight();
                    selectedDave = null;
                }
                
                //Go to pickup
                else if (selectedDave && hit.transform.TryGetComponent(out Pickup pickup))
                {
                    selectedDave.SetDestination(pickup.transform.position);
                    selectedDave.UnHighlight();
                    selectedDave = null;
                }

                //unselect if nothing clicked
                else if(selectedDave)
                {
                    selectedDave.UnHighlight();
                    selectedDave = null;
                }
            }
        }
    }
}
