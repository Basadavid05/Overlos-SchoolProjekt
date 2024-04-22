using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerato : MonoBehaviour
{
    public GameObject[] trees;
    private Collider coll;
    private LayerMask Ground;

    private float width, height;

    [Range(0.01f,10f)]
    public float acceptancePoint;

    public int seed;

    [Range(0.01f, 10f)]
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
        Ground= LayerMask.GetMask("Ground");
        width = coll.bounds.size.x*100;
        height = coll.bounds.size.z*100;
        TreeGenerate();
    }

    private void TreeGenerate()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float xvalue = x / scale;
                float yvalue = y / scale;

                float perlinvalue = Mathf.PerlinNoise(xvalue + seed, yvalue + seed);
                Debug.Log(perlinvalue);

                if (perlinvalue >= acceptancePoint)
                {
                    if (Physics.Raycast(new Vector3(x,100,y),Vector3.down,out RaycastHit hit,200f,Ground))
                    {
                        float yPoint;
                        yPoint=hit.point.y;
                        GameObject tree = Instantiate(trees[0],new Vector3(x,yPoint,y),Quaternion.identity);
                        tree.transform.SetParent(transform);
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
