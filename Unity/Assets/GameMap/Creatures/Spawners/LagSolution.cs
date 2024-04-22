using UnityEngine;
using UnityEngine.AI;

public class LagSolution : MonoBehaviour
{
    private MonoBehaviour[] scripts;

    private void Start()
    {
        scripts = transform.GetComponents<MonoBehaviour>();
        LayerMask layerMask = LayerMask.GetMask("Detection");
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, 0.05f, layerMask);
        // Check if there is at least one collider nearby
        if (nearbyColliders.Length <= 0)
        { OverDisable(); }

    }

    public void OverDisable()
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
            if (transform.TryGetComponent<MeshRenderer>(out MeshRenderer rg))
            {
                rg.enabled = true;
            }
            foreach (var script in scripts)
            {
                script.enabled = true;
            }
            if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = false;
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            if (transform.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent)){
                agent.enabled = true;
            }
    }

    


}
