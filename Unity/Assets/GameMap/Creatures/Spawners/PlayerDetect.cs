using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    private Collider coll;
    private bool playerdetect=false;
    private bool doubleCheck=false;
    private LayerMask layerMask;
    private void Start()
    {
        coll = GetComponent<Collider>();
        layerMask = LayerMask.GetMask("Detection");
    }


}
