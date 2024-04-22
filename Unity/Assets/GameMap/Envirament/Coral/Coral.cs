using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainUtils;

public class Coral : MonoBehaviour
{
    private float mapWith;
    private float MapHeight;

    [Header("Uniqueness")]
    [Range(0.5f, 2f)]
    public float scale;
    private LayerMask GroundMask;
    private LayerMask OceanMask;
    public int seed;

    [Header("Objects")]
    public List<GameObject> ListOfObject = new List<GameObject>();

    [Header("Nem tom")]
    [Range(0.5f, 2f)]
    public float acceptancepoint;

    SpawnReferences SpawnReferences;

    void Start()
    {
        ConvertMapValues();

        for (int i = 0; i < ListOfObject.Count; i++)
        {
            GameObject gameObject = new GameObject(ListOfObject[i].name);
            gameObject.transform.SetParent(transform);
        }

        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < mapWith; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX + seed, sampleY + seed);
                if (perlinValue >= acceptancepoint)
                {
                    if (Physics.Raycast(new Vector3(x, 250, y), Vector3.down, out RaycastHit hit, 250f, GroundMask))
                    {
                        float yPoint;
                        yPoint = hit.point.y;
                        int count = GetRandomIndex();
                        GameObject Coral = Instantiate(ListOfObject[count], new Vector3(x, yPoint, y), quaternion.identity);
                        Coral.transform.SetParent(transform.GetChild(count));
                    }
                }
            }
        }
    }

    private int GetRandomIndex() //Random Number for to chose a prefab 
    {
        System.Random random = new System.Random();
        Debug.Log(ListOfObject.Count);
        int randomNumber = random.Next(0, ListOfObject.Count);
        return randomNumber;
    }

    public void ConvertMapValues()
    {
        SpawnReferences = transform.parent.GetComponent<SpawnReferences>();
        GroundMask = SpawnReferences.Ground;
        OceanMask = SpawnReferences.Water;
        /*
        float oceanTerrainHeightDifference = waterReferences.Oceanbounds.max.y - waterReferences.terrainBounds.min.y;
        MapHeight = Mathf.RoundToInt(oceanTerrainHeightDifference);
        */

        // Calculate map dimensions based on ocean and terrain bounds
        Vector3 TerrainMax = SpawnReferences.terrainBounds.max;
        Vector3 terrainMin = SpawnReferences.terrainBounds.min;

        // Calculate map width and height
        Debug.Log(TerrainMax.x);
        Debug.Log(terrainMin.x);
        Debug.Log(TerrainMax.z);
        Debug.Log(terrainMin.z);
        //mapWith = Mathf.RoundToInt(TerrainMax.x - terrainMin.x);
        //MapHeight = Mathf.RoundToInt(TerrainMax.z - terrainMin.z);
        mapWith=TerrainMax.x;
        MapHeight=TerrainMax.z;

    }
}
