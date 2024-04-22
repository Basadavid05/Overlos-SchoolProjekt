using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sp2 : MonoBehaviour
{
    [SerializeField]
    public GameObject[] enemyPrefab;
    public float spawnRate = 5f;
    public float spawnDistance = 5f;
    public float spawnCount = 0f;
    public float MaxSpawn = 10f;
    private bool canSpawn = true;

    private List<List<GameObject>> spawnedEnemiesLists = new List<List<GameObject>>();

    IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            spawnedEnemiesLists.Add(new List<GameObject>());
        }

        while (canSpawn && spawnCount < MaxSpawn)
        {
            yield return wait;

            int rand = Random.Range(0, enemyPrefab.Length);
            GameObject enemyToSpawn = enemyPrefab[rand];

            Vector3 spawnPos = GetValidSpawnPoint();

            GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
            spawnedEnemiesLists[rand].Add(spawnedEnemy);
            Debug.Log(spawnedEnemiesLists);

            spawnCount++;
        }
    }

    Vector3 GetValidSpawnPoint()
    {
        Vector3 randomSpawnPoint;
        do
        {
            randomSpawnPoint = new Vector3(Random.Range(395, 650), 72, Random.Range(600, 1073));
        } while (!IsSpawnPointAboveWater(randomSpawnPoint));

        return randomSpawnPoint;
    }

    bool IsSpawnPointAboveWater(Vector3 spawnPoint)
    {
        Ray ray = new Ray(spawnPoint + Vector3.up * 10f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 20f))
        {
            if (hit.collider.CompareTag("Water"))
            {
                return false; // Spawn point is in water
            }
        }
        return true; // Spawn point is above water
    }

    private void OnTriggerEnter(Collider box)
    {
        if (box.CompareTag("Player"))
        {
            StartCoroutine(SpawnEnemies());
        }
    }
}

