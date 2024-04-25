using UnityEngine;

public class SpawnReferences : MonoBehaviour
{
    [HideInInspector] public LayerMask Ground;

    private float MinScale = 0.5f;
    private float MaxScale = 2.2f;

    public static bool Respawnenemy;

    private void Start()
    {
        Respawnenemy=false;
        Ground = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        if(MapControl.MapIsEnabled)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (Respawnenemy)
        {
            Invoke("Enemy", 0.1f);
        }
    }

    private void Enemy()
    {
        Respawnenemy = false;
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


