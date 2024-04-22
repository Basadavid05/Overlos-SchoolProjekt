using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway")]
    [SerializeField] float Smooth;
    [SerializeField] float Multipler;

    public float rotationSpeed = 5.0f;


    /*[Header("Mouse")]
    float MouseX, MouseY;
    float xRotation, yRotation;*/

    // Update is called once per frame
    void Update()
    {


        /*float MouseX = Input.GetAxisRaw("Mouse X")*Multipler;
        float MouseY = Input.GetAxisRaw("Mouse Y") * Multipler;
        
        Quaternion xRotation = quaternion.AxisAngle(Vector3.right ,- MouseY);
        Quaternion yRotation = quaternion.AxisAngle(Vector3.up,MouseX);

        Quaternion TargetLocation = xRotation * yRotation;

        transform.rotation = Quaternion.Slerp(transform.localRotation, TargetLocation, Time.deltaTime);
        

        MouseX = Input.GetAxisRaw("Mouse X");
        MouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += MouseX * MouseSpeed * Time.deltaTime;
        xRotation -= MouseY * MouseSpeed * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -90, 90f);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);*/
        /*

        // Get the position of the mouse or touch input
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to a point in the game world
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10.0f));

        // Calculate the direction from the weapon to the target position
        Vector3 direction = targetPosition - transform.position;

        // Calculate the rotation angle based on the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create a rotation based on the angle
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Smoothly rotate the weapon towards the target direction
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        */
    }
}
