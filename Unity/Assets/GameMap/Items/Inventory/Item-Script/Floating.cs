using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    private Transform obj;
    private Rigidbody rb;
    public float floatHeight = 1f;  // Set the height at which the item should float
    public float floatSpeed = 1f;   // Set the speed of the floating effect
    public float detectionRange = 5f;  // Set the range at which the item detects the player
    public float rotationSpeed = 40f; // Rotation speed when facing the player


    private void Start()
    {
        obj = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        StartCoroutine(FloatObject());
    }

    private IEnumerator FloatObject()
    {
        Vector3 startPos = obj.position;
        while (true)
        {
            // Float the object up and down
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            obj.position = new Vector3(startPos.x, newY, startPos.z);
            
            // Check for player within the detection range
            Collider[] colliders = Physics.OverlapSphere(obj.position, detectionRange, LayerMask.GetMask("Player"));

            if (colliders.Length > 0)
            {
                // Get the player's position
                Vector3 playerPosition = colliders[0].transform.position;

                // Adjust the item's rotation to face the player
                //obj.LookAt(playerPosition);

                // Optional: You may want to freeze rotation along certain axes
                // obj.rotation = Quaternion.Euler(0f, obj.eulerAngles.y, 0f);

                // Adjust the item's rotation to face the player smoothly
                Quaternion targetRotation = Quaternion.LookRotation(playerPosition - obj.position);
                obj.rotation = Quaternion.Slerp(obj.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            yield return null;
        }
    }
}
