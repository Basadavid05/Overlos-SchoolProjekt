using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform CameraPosition;

    void Update()
    {
        transform.position = CameraPosition.position;
        transform.rotation = CameraPosition.rotation;
    }
}
