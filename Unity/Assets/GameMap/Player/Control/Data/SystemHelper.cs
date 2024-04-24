using UnityEngine;

public class SystemHelper : MonoBehaviour
{
    private Collider coll;
    private Rigidbody rb;
    private void Start()
    {
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Creatures") || other.CompareTag("Enemy") || other.CompareTag("Envirament"))
        {
            LagSolution lagSolution = other.gameObject.GetComponent<LagSolution>();
            Area areas= other.gameObject.GetComponent<Area>();
            if (lagSolution != null)
            {
                lagSolution.OverDisable();
            }
            if (areas != null)
            {
                areas.ChildrenChange(false);
            }


        }
    }


}
