using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WeaponLook : MonoBehaviour
{
    [Header("Control")]
    float MoveSpeed;
    private float MouseX;
    private float MouseZ;
    Vector3 movement;
    float turnSpeed = 10f;

    [SerializeField] Transform orientation;


    void Update()
    {
        MouseX = Input.GetAxis("Horizontal");
        MouseZ = Input.GetAxis("Vertical");
        movement = (MouseZ * orientation.forward + MouseX * orientation.right).normalized;

        if (movement != Vector3.zero)
        {
            // Rotate the character to face the movement direction
            Quaternion newRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, turnSpeed * Time.deltaTime);
        }
    }
}
