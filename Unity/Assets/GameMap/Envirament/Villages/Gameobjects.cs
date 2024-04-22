using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameobjects : MonoBehaviour
{
    void Start()
    {
        CombineShowDelete(transform);
    }

    void CombineShowDelete(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Combine meshes
            CombineMeshes(child);

            // Show the child object
            child.gameObject.SetActive(true);

            // Delete the child object after 1 second
            Destroy(child.gameObject, 1f);

            // If the current child has children, recursively call this function for each child
            if (child.childCount > 0)
            {
                CombineShowDelete(child);
            }
        }
    }

    void CombineMeshes(Transform obj)
    {
        // Get all MeshFilter components attached to the object
        MeshFilter[] meshFilters = obj.GetComponentsInChildren<MeshFilter>();

        // Combine meshes
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            // Check for null reference before accessing mesh
            if (meshFilters[i] != null && meshFilters[i].sharedMesh != null)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false); // Deactivate child objects
            }
        }

        // Create a new mesh filter and renderer for the combined mesh
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine, true);

        // Create a new game object to hold the combined mesh
        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.position = obj.position;
        combinedObject.transform.rotation = obj.rotation;
        combinedObject.transform.localScale = obj.localScale;
        combinedObject.AddComponent<MeshFilter>().sharedMesh = combinedMesh;
        combinedObject.AddComponent<MeshRenderer>().sharedMaterial = obj.GetComponentInChildren<MeshRenderer>().sharedMaterial;
    }

}
