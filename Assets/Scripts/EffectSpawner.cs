using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject spawnObject;
    [SerializeField] float spawnHeightRange;
    float rate;
    private Camera mainCamera;

    #endregion

    void Start()
    {
        //get camera
        mainCamera = Camera.main;
    }

    void Update()
    {
    }

    public void Spawn(string text)
    {
        var status = Instantiate(spawnObject,
            new Vector3(
                this.transform.position.x,
                this.transform.position.y,
                this.transform.position.z),
            transform.rotation, this.transform);
        

        status.GetComponentInChildren<TextMeshPro>().text = text;
        
        Destroy(status, 10);
    }

    float GetModifier()
    {
        float modifier = Random.Range(0f, spawnHeightRange);
        if (Random.Range(0, 2) > 0)
            return -modifier;
        else
            return modifier;
    }
}