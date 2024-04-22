using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

    private int objectcanbepickup1;
    private int objectcanbepickup2;

    Rigidbody rb;

    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectcanbepickup1 = Random.Range(1, 9);
        objectcanbepickup2 = Random.Range(991, 999);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (targetHit)
            return;
        else
            targetHit = true;

        if (collision.gameObject.GetComponent<BasiccEnemy>()!=null)
        {
            BasiccEnemy enemy=collision.gameObject.GetComponent<BasiccEnemy>();
            enemy.TakeDamage(damage);
            if (objectcanbepickup1 > 5 && objectcanbepickup2 > 994)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.SetParent(collision.transform);
                rb.isKinematic = true;
            }
        }
        else
        {
            transform.SetParent(ItemPlacement.instance.ItemDropLocation);
        }

    }
    public float treshold;

    void FixedUpdate()
    {
        if (transform.position.y < treshold)
        {
            Destroy(gameObject);
        }
    }

}
