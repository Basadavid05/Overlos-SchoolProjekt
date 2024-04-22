using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    Rigidbody rb;

    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(rb.transform.position.y<-15)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (targetHit)
            return;
        else
            targetHit = true;

        if (collision.gameObject.GetComponent<BasiccEnemy>() != null)
        {
            BasiccEnemy enemy = collision.gameObject.GetComponent<BasiccEnemy>();

            enemy.TakeDamage(damage);

        }
        else
        {
            Destroy(gameObject);
        }


        rb.isKinematic = true;
        transform.SetParent(collision.transform);

    }
}
