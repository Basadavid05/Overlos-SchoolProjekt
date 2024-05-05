using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine;

public class BuildingCombinator : MonoBehaviour
{
    public List<MeshFilter> listMeshFilters;
    public List<Material> materials;
    public List<Transform> transforms;

    public MeshFilter target;
    private int count;

    // Start is called before the first frame update
    private void Start()
    {
        CollectMeshFiltersAndTransforms();
    }

    private void CollectMeshFiltersAndTransforms()
    {
        int childCount = transform.childCount;
        GameObject Details = new GameObject("Detail");
        Details.transform.parent = transform;
        Details.transform.SetSiblingIndex(0);
        foreach (Transform child in transform)
        {
            child.gameObject.isStatic = false;
            MeshFilter meshFilter = child.GetComponent<MeshFilter>();
            Light meshLight = child.GetComponent<Light>();
            if (meshFilter != null && meshLight == null)
            {
                listMeshFilters.Add(meshFilter);
                transforms.Add(child);
                child.parent = Details.transform;
            }
            child.gameObject.isStatic = true;
        }


    }

    private void CombineMeshes()
    {
        // Gyűjtsük össze a mesh-eket és a transzformációkat a CombineInstance-ekbe
        CombineInstance[] combineInstances = new CombineInstance[listMeshFilters.Count];
        for (int i = 0; i < listMeshFilters.Count; i++)
        {
            combineInstances[i].mesh = listMeshFilters[i].sharedMesh;
            combineInstances[i].transform = listMeshFilters[i].transform.localToWorldMatrix;
        }

        // Kombináljuk a mesh-eket
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances, true, true);

        // Hozzunk létre egy új GameObject-et a kombinált mesh-sel
        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.parent = transform;

        // Adjunk hozzá egy MeshFilter komponenst és állítsuk be a kombinált mesh-t
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        meshFilter.mesh = combinedMesh;

        // Adjunk hozzá egy MeshRenderer komponenst és állítsuk be az anyagot (például az első gyermek objektum anyagát)
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();
        meshRenderer.material = listMeshFilters[0].GetComponent<MeshRenderer>().sharedMaterial;

        // Adjunk hozzá egy MeshCollider komponenst
        MeshCollider meshCollider = combinedObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = combinedMesh;
    }





    // Update is called once per frame
    private void Update()
    {
        
    }
}

/*
 *listmeshfilters.Add(transform.GetChild(i).GetComponent<MeshFilter>());
            transforms.Add(transform.GetChild(i).transform);
            //Destroy(transform.GetChild(i).gameObject);
            combine[i].mesh = transform.GetChild(i).GetComponent<MeshFilter>().sharedMesh;
            combine[i].transform = transform.GetChild(i).transform.localToWorldMatrix;

            var mesh=new Mesh();

            mesh.CombineMeshes(combine);
 *
 */
