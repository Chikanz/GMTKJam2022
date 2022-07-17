using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideEffectBehaviour : MonoBehaviour
{

    #region Variables
    [SerializeField] float Speed;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //calculate the Angle forward needs to face
        // var rot = Quaternion.LookRotation(Vector3.back, Vector3.up);
        //the -90f might need to change in future
        // transform.rotation = Quaternion.Euler(new Vector3(0, 90f, -(rot.z - Target.transform.eulerAngles.z)));
    }

    // Update is called once per frame
    void Update()
    {
        //move the object up
        transform.position += Vector3.up * Speed * Time.deltaTime;

        // MoveTowards(Target.transform.position);
        //apply status effect/change when this obj hits the target?
    }

    void MoveTowards(Vector3 target)
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.x, Speed * Time.deltaTime), Mathf.Lerp(transform.position.y, target.y, Speed * Time.deltaTime), Mathf.Lerp(transform.position.z, target.z, Speed * Time.deltaTime));
    }
}
