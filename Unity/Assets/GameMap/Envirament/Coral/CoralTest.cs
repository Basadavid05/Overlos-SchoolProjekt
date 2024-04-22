using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Publics")]
    public GameObject[] Objects;
    public int MaxSpawn = 500;

    [Header("Privates")]
    private SpawnReferences SpawnReferences;
    private Vector3 randomPosition;

    [Header("Bounds")]
    private Bounds terrainBounds;
    private Bounds Oceanbounds;

    private void Start()
    {
        SpawnReferences = transform.parent.GetComponent<SpawnReferences>();
        terrainBounds = SpawnReferences.terrain.GetComponent<Collider>().bounds;
        Oceanbounds = SpawnReferences.Ocean.GetComponent<Collider>().bounds;

        for (int i = 0; i < Objects.Length; i++)
        {
            GameObject gameObject = new GameObject(Objects[i].name);
            gameObject.transform.SetParent(transform);
        }
        for (int i = 0; i < MaxSpawn; i++)
        {
            Spawning();
        }
    }
    private void Spawning()
    {
        int rand = Random.Range(0, Objects.Length);
        GameObject enemyToSpawn = Objects[rand];
        Vector3 spawnPos = GetValidSpawnPoint();
        GameObject Enemy = Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
        Enemy.transform.localScale = Vector3.one * Random.Range(.5f, 1.6f);
        SpawnReferences.RotateObjectToGroundNormal(Enemy, spawnPos);
        //SpawnReferences.GameObjectScale(Enemy);
        Enemy.transform.SetParent(transform.GetChild(rand));
    }

    Vector3 GetValidSpawnPoint()
    {
        do
        {
            // Generate random x, y, and z coordinates within the bounds of the collider
            float randomX = UnityEngine.Random.Range(terrainBounds.min.x + 54, terrainBounds.max.x - 54);
            float randomY = UnityEngine.Random.Range(Oceanbounds.min.y + 5, Oceanbounds.max.y - 3f);
            float randomZ = UnityEngine.Random.Range(terrainBounds.min.z + 64, terrainBounds.max.z - 64);

            // Create a new Vector3 with the random coordinates
            randomPosition = new Vector3(randomX, randomY, randomZ);
        } while (!PrayForWater() && !SpawnHelp());
        YSetting();
        return randomPosition;
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
            else { }
        }
        return true;
    }
    private void YSetting()
    {
        RaycastHit hit;
        if (Physics.Raycast(randomPosition + new Vector3(0, -350, 0), Vector3.up, out hit, 350, SpawnReferences.Ground))
        {
            randomPosition.y = hit.point.y + 0.3f;
        }
        if (Physics.Raycast(randomPosition, Vector3.down, out hit, 150, SpawnReferences.Ground))
        {
            randomPosition.y = hit.point.y+0.3f;
        }
        
    }


}