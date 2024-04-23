using UnityEngine;
using UnityEngine.AI;

public class LagSolution : MonoBehaviour
{
    private MonoBehaviour[] scripts;
    private LayerMask layerMask;
    private bool mapisenabled;
    private bool PlayerIsThere;
    private Collider coll;

    private void Start()
    {
        if (coll != null)
        {
            coll = GetComponent<Collider>();
        }
        else
        {
            coll = gameObject.AddComponent<BoxCollider>();
            // Set the size of the BoxCollider
            BoxCollider boxCollider = (BoxCollider)coll;
            boxCollider.size = new Vector3(0.1f, 0.1f, 0.1f);
        }
        coll.isTrigger = true;
        scripts = transform.GetComponents<MonoBehaviour>();
        layerMask = LayerMask.GetMask("Detection");
        Invoke("Check", 3f);
    }

    private void OnEnable()
    {
        scripts = transform.GetComponents<MonoBehaviour>();

        Invoke("Check", 3f);
    }

    private void Check()
    {
        coll.enabled = false; coll.enabled=true;
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, 0.05f, layerMask);
        // Check if there is at least one collider nearby
        if (nearbyColliders.Length <= 0)
        { OverDisable(); }
    }

    public void OverDisable()
    {
        if (scripts.Length > 1)
        {
            foreach (var script in scripts)
            {
                // Skip the DisableScriptsForGameObject script itself
                if (script.GetType() == typeof(LagSolution))
                    continue;
                else if (script.GetType() == typeof(Collider))
                    continue;
                script.enabled = false;
            }
        }
        
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if (transform.TryGetComponent<MeshRenderer>(out MeshRenderer rg))
        {
            rg.enabled = false;
        }
        if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
        if(transform.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            agent.enabled = false;
        }
    }

    public void OverEnable()
    {

           foreach (var script in scripts)
            {
                script.enabled = true;
            }

              // Check if MeshRenderer exists before accessing it
        MeshRenderer rg = transform.GetComponent<MeshRenderer>();
        if (rg != null)
        {
            rg.enabled = true;
        }

        // Check if Rigidbody exists before accessing it
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // Activate all child game objects
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        // Check if NavMeshAgent exists before accessing it
        NavMeshAgent agent = transform.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = true;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerMask)
        {
            OverEnable();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!PlayerIsThere)
        {
            if (other.gameObject.layer == layerMask)
            {
                OverEnable();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!PlayerIsThere && other.gameObject.layer == layerMask)
        {
            OverDisable();
        }
    }
    */



}
