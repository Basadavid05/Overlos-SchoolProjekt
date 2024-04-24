using System.Collections;
using UnityEngine;

public class Sp1 : MonoBehaviour
{
    [SerializeField]
    public GameObject[] enemyPrefab;
    private float spawnRate = 5f;
    public float spawnCount = 0f;
    public int MaxSpawn;
    private bool canSpawn = true;
    private int spawn = 0;
    
    [Header("In Water")]
    public bool InWater;
    private LayerMask Ground;
    private LayerMask Water;

    [Header("Checks")]
    private bool CanThereSpawn;
    private float width, height;
    private float max;

    [Header("Privates")]
    private SpawnReferences SpawnReferences;
    private Vector3 spawnPosition;
    private Collider Coll;

    private GameObject Title;

    private void Start()
    {
        Ground = LayerMask.GetMask("Ground");
        Water = LayerMask.GetMask("Water");
        Coll = transform.parent.GetComponent<Collider>();
        SpawnReferences = transform.parent.parent.GetComponent<SpawnReferences>();
        width = Coll.bounds.size.x;
        height = Coll.bounds.size.z;
        max = Coll.bounds.max.y;
        if (spawn == 0)
        {
            Title = new GameObject("Enemy");
            Title.transform.parent = transform;
            if (spawnCount < 1 && spawnCount < MaxSpawn)
            {
                for (int i = 0; i < enemyPrefab.Length; i++)
                {
                    GameObject gameObject = new GameObject(enemyPrefab[i].name);
                    gameObject.tag = "Enemy";
                    gameObject.transform.SetParent(Title.transform);
                }

                for (int i = 0; i < MaxSpawn / 3; i++)
                {
                    Spawning();
                }
            }
            spawn++;
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
        GameObject Enemy = Instantiate(enemyPrefab[rand], GetValidSpawnPoint(), Quaternion.identity);
        Enemy.tag = "Enemy";
        SpawnReferences.RotateObjectToGroundNormal(Enemy, spawnPosition);
        Enemy.transform.localScale = Enemy.transform.localScale * Random.Range(.7f, 1.6f);
        Enemy.transform.SetParent(Title.transform.GetChild(rand));
        spawnCount++;
    }

    Vector3 GetValidSpawnPoint()
    {
        do
        {
            CanThereSpawn = true;
            // Generate random position within the collider bounds
            float randomX = Random.Range(-width / 2f, width / 2f);
            float randomZ = Random.Range(-height / 2f, height / 2f);
            spawnPosition = transform.position + new Vector3(randomX, 0, randomZ);
            spawnPosition.y = max;
            if (Physics.Raycast(spawnPosition, Vector3.down, out RaycastHit hit, max * 2, Ground))
            {
                spawnPosition.y = hit.point.y+2;
            }
            else
            {
                CanThereSpawn = false;
            }

        } while (Watertest(spawnPosition) && CanThereSpawn);
        return spawnPosition;
    }

    private bool Watertest(Vector3 spawnPoint)
    {
        if (!InWater)
        {
            return Physics.CheckSphere(spawnPoint + new Vector3(0, 1, 0), 0.1f, Water);
        }
        else
        {
            return !Physics.CheckSphere(spawnPoint + new Vector3(0, 1, 0), 0.1f, Water);
        }
    }


    private void NewSpawnRate()
    {
        spawnRate = Random.Range(0,30f);
    }

}

