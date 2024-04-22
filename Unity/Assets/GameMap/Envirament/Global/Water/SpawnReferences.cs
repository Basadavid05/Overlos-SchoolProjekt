using UnityEngine;

public class SpawnReferences : MonoBehaviour
{
    [Header("Ground")]
     public GameObject Ocean;
     public Terrain terrain;
     [HideInInspector]public Bounds terrainBounds;
     [HideInInspector]public Bounds Oceanbounds;

     [Header("Ocean")]
    [HideInInspector] public LayerMask Water;
    [HideInInspector] public LayerMask Ground;

    private float MinScale = 0.5f;
    private float MaxScale = 2.2f;

    private void Start()
    {
        Ground = LayerMask.GetMask("Ground");
        Water = LayerMask.GetMask("Water");
    }


    public void GameObjectScale(GameObject gameObject)
    {

        float[] vectors=new float[3];
        float random = Random.Range(MinScale, MaxScale);
        Vector3 previousScale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(previousScale.x*random, previousScale.y * random, previousScale.z * random);
    }

    public void RotateObjectToGroundNormal(GameObject obj, Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position+new Vector3(0,2,0), Vector3.down, out hit, 10f, Ground))
        {
            obj.transform.up = hit.normal;
        }
    }

}


