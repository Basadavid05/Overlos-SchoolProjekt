using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checker : MonoBehaviour
{
    private void Start()
    {
        Ray ray1 = new Ray(transform.position + new Vector3(0, 100, 0), Vector3.down);
        RaycastHit[] hits1 = Physics.RaycastAll(ray1, 100f);

        Debug.Log(hits1.Length);

        Ray ray2 = new Ray(transform.position, Vector3.up);
        RaycastHit[] hits2 = Physics.RaycastAll(ray2, 100f);

        Debug.Log(hits2.Length);

        for (int i = 0; i < hits1.Length; i++)
        {
            Debug.Log(hits1[i].collider.name);
        }
        for (int i = 0; i < hits2.Length; i++)
        {
            Debug.Log(hits2[i].collider.name);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log(colliders[i].name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name != "Ocean")
        {
            Debug.Log(collision.collider.name);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.name != "Ocean")
        {
            Debug.Log(collision.collider.name);
        }
    }
}
