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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Creatures") || other.CompareTag("Enemy") || other.CompareTag("Envirament"))
        {
            LagSolution lagSolution = other.gameObject.GetComponent<LagSolution>();
            if (lagSolution != null)
            {
                lagSolution.OverEnable();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Creatures") || other.CompareTag("Enemy") || other.CompareTag("Envirament"))
        {
            LagSolution lagSolution = other.gameObject.GetComponent<LagSolution>();
            if (lagSolution != null)
            {
                lagSolution.OverEnable();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Creatures") || other.CompareTag("Enemy") || other.CompareTag("Envirament"))
        {
            LagSolution lagSolution = other.gameObject.GetComponent<LagSolution>();
            if (lagSolution != null)
            {
                lagSolution.OverDisable();
            }
        }
    }


}
