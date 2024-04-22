using Unity.VisualScripting;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    private Collider coll;
    public int knowbacky;
    public int knowbackxz;
    private Rigidbody rb;
    public int Damage;

    public bool Attack;


    private void Start()
    {
        coll = GetComponent<Collider>();
        rb=transform.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        Attack = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Playerskin")
        {
            if (Attack)
            {
                PlayerDatas.Takedamage(Damage);
                Attack = false;
                /*
                Vector3 knockbackDirection = (-transform.forward).normalized;
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(knockbackDirection-new Vector3(knowbackxz, knowbacky,knowbackxz), ForceMode.Impulse);
                */
            }
        } 
    }


}
