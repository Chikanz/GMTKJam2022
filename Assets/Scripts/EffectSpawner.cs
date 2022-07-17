using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    #region Variables
    public GameObject spawnObject;
    public float spawnTimer;
    [SerializeField] float spawnHeightRange;
    float rate;
    #endregion
    void Start()
    {
        rate = spawnTimer;
    }

    void Update()
    {
         Spawn();
    }

    void Spawn()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0f)
        {
            Instantiate(spawnObject,new Vector3(this.transform.position.x, this.transform.position.y + GetModifier(), this.transform.position.z), Quaternion.identity, this.transform);
            spawnTimer = rate;
        }
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
