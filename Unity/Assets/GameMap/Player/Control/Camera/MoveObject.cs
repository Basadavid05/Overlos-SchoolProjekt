using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveObject : MonoBehaviour
{
    public Transform Object;

    private void Update()
    {
        transform.position = Object.position;
        transform.rotation = Object.rotation;
    }
}
