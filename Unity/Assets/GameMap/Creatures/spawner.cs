using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject spawnee;
    public bool stopSpawn = false;
    public float spawnTime;
    public float snum;

    void Start()
    {
        Invoke("SpawnObject", spawnTime);
    }

    public void SpawnObject()
    {
        for (int i = 1; i <= spawnTime; i++)
        {
            Instantiate(spawnee, transform.position, transform.rotation);
            snum++;
        }

        if (snum <= spawnTime)
        {
            stopSpawn = true;
            CancelInvoke("SpawnObject");
        }
    }
}
