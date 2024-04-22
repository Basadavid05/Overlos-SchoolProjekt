using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLook_Camera : MonoBehaviour
{
    [Header("Gameobjects")]
    [SerializeField] Transform firstpersoncam;

    [Header("Mouse")]
    public float MouseX, MouseY;
    public float xRotation;
    public float yRotation;
    [HideInInspector] public float MouseSpeed = 1000;
    public float smoothTime = 0.2f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FirstpersonsInput()
    {
        MouseX = Input.GetAxisRaw("Mouse X") * MouseSpeed  * Time.deltaTime;
        MouseY = Input.GetAxisRaw("Mouse Y") * MouseSpeed  * Time.deltaTime;

        yRotation += MouseX;
        xRotation -= MouseY;

        xRotation = Mathf.Clamp(xRotation, -90, 90f);
    }

    private void Update()
    {
        FirstpersonsInput();
    }
    private void FixedUpdate()
    {
        firstpersoncam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}

