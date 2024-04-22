using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdMoveCamera : MonoBehaviour
{
    public Transform ThirdPosition;
    void Update()
    {
        transform.position = ThirdPosition.position;
    }
}
