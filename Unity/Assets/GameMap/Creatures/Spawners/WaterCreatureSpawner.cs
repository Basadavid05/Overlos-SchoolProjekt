using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class WaterCreatureSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private float spawnRate = 5f;
    public int spawnCount = 0;
    public int MaxSpawn = 30;
    private bool canSpawn = true;

    [Header("Privates")]
    private SpawnReferences SpawnReferences;
    private Vector3 randomPosition;

    [Header("Terrain")]
    public GameObject Ocean;
    public Terrain terrain;

    [Header("Bounds")]
    private Bounds terrainBounds;
    private Bounds Oceanbounds;

    private void Start()
    {
        SpawnReferences = transform.parent.Find("Areas").GetComponent<SpawnReferences>();
        terrainBounds = SpawnReferences.terrain.GetComponent<Collider>().bounds;
        Oceanbounds = SpawnReferences.Ocean.GetComponent<Collider>().bounds;

        if (spawnCount < 1 && spawnCount < MaxSpawn)
        {
            for (int i = 0; i < enemyPrefab.Length; i++)
            {
                GameObject gameObject = new GameObject(enemyPrefab[i].name);
                gameObject.transform.SetParent(transform);
            }

            for (int i = 0; i < MaxSpawn / 3; i++)
            {
                Spawning();
            }
        }
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        NewSpawnRate();
        while (canSpawn && spawnCount < MaxSpawn)
        {
            yield return wait;
            Spawning();
        }
    }
    private void Spawning()
    {
        int rand = Random.Range(0, enemyPrefab.Length);
        GameObject enemyToSpawn = enemyPrefab[rand];
        Vector3 spawnPos = GetValidSpawnPoint();
        GameObject Enemy= Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
        SpawnReferences.RotateObjectToGroundNormal(Enemy, spawnPos);
        SpawnReferences.GameObjectScale(Enemy);
        Enemy.transform.SetParent(transform.GetChild(rand));
        spawnCount++;
    }

    Vector3 GetValidSpawnPoint()
    {
            do
            {
                // Generate random x, y, and z coordinates within the bounds of the collider
                float randomX = UnityEngine.Random.Range(terrainBounds.min.x+54, terrainBounds.max.x-54);
                float randomY = UnityEngine.Random.Range(Oceanbounds.min.y+5, Oceanbounds.max.y - 3f);
                float randomZ = UnityEngine.Random.Range(terrainBounds.min.z +64, terrainBounds.max.z -64);

                // Create a new Vector3 with the random coordinates
                randomPosition = new Vector3(randomX, randomY, randomZ);
            } while (!PrayForWater() && !SpawnHelp());

            return randomPosition;
    }

    private void NewSpawnRate()
    {
        spawnRate = Random.Range(0, 30f);
    }

    private bool SpawnHelp()
    {
        // Az objektum körül lévő kis kör alakú terület ellenőrzése a föld réteggel való érintkezésre
        Collider[] colliders = Physics.OverlapSphere(transform.position, 7f, SpawnReferences.Ground);

        // Ha talál érintkezést, akkor az objektum a földön van
        if (colliders.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool PrayForWater()
    {
        Ray ray1 = new Ray(randomPosition + new Vector3(0, 250f, 0), Vector3.down);
        RaycastHit[] hits1 = Physics.RaycastAll(ray1, 250f);

        Ray ray2 = new Ray(randomPosition, Vector3.up);
        RaycastHit[] hits2 = Physics.RaycastAll(ray2, 250f);

        if (hits2.Length > 1 && hits1.Length > 1)
        {
            return false;
        }

        for (int i = 0; i < hits1.Length; i++)
        {
            if (hits1[i].collider.gameObject.name != "Ocean")
            {
                return false;
            }
        }

        for (int i = 0; i < hits2.Length; i++)
        {
            if (hits2[i].collider.gameObject.name != "Ocean")
            {
                return false;
            }
            else{}
        }
        return true;
    }


}
