using UnityEngine;

public class SpawnEnviramentObj : MonoBehaviour
{
    [Header("Objects")]
    public GameObject[] SpawningObj;

    [Header("Privates")]
    private LayerMask Ground;
    private LayerMask Water;
    private float width, height;
    private int counter;
    private int maximum;
    private float max;

    [Header("Colliders")]
    private Vector3 spawnPosition;
    private Collider Coll;
    private SpawnReferences spawnReferences;
    private GameObject Title;


    [Header("Publics")]
    public bool InWater;
    private int spawn=0;
    private bool CanThereSpawn;

    public enum type
    {

        trees=250,
        forest=1013,
        coral=413,
    }

    public type selectedType;



    private void Start()
    {
        Ground = LayerMask.GetMask("Ground");
        Water = LayerMask.GetMask("Water");
        Coll = transform.parent.GetComponent<Collider>();
        width = Coll.bounds.size.x;
        height = Coll.bounds.size.z;
        max = Coll.bounds.max.y;
        spawnReferences = transform.parent.parent.GetComponent<SpawnReferences>();
        if (spawn == 0)
        {
            Title = new GameObject("Envirament");
            Title.transform.parent = transform;

            for (int i = 0; i < SpawningObj.Length; i++)
            {
                GameObject gameObject = new GameObject(SpawningObj[i].name);
                gameObject.transform.SetParent(Title.transform);
                gameObject.transform.localScale = transform.localScale * Random.Range(0.5f, 2.2f);
            }


            maximum = (int)selectedType;
            ObjectGenerate();
            spawn++;
            }
    }

    private void ObjectGenerate()
    {
        counter = 0;
        while (counter < maximum)
            {
                do
                {
                    CanThereSpawn = true;
                    // Generate random position within the collider bounds
                    float randomX = Random.Range(-width / 2f, width / 2f);
                    float randomZ = Random.Range(-height / 2f, height / 2f);
                    spawnPosition = transform.position + new Vector3(randomX, 0, randomZ);
                    spawnPosition.y = max;
                    if (Physics.Raycast(spawnPosition, Vector3.down, out RaycastHit hit, max*2, Ground))
                    {
                        spawnPosition.y = hit.point.y;
                    }
                    else
                    {
                        CanThereSpawn=false;
                    }

            } while (Watertest(spawnPosition) && CanThereSpawn);
            int SpawnObj = Random.Range(0, SpawningObj.Length);
            GameObject objToSpawn = Instantiate(SpawningObj[SpawnObj], spawnPosition, Quaternion.identity, Title.transform.GetChild(SpawnObj));
            objToSpawn.tag = "Envirament";
            counter++;

        }

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
}
