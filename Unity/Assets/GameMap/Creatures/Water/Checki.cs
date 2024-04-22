using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Checki : MonoBehaviour
{
    private void Start()
    {
        check();
    }
    // Update is called once per frame
    private void check()
    {
        Ray RayUp = new Ray(transform.localPosition + new Vector3(0, 100, 0), Vector3.down);
        if (Physics.Raycast(RayUp, out RaycastHit hitter, 103f))
        {
            if (hitter.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                //Debug.Log(hitter.collider.gameObject.name);
            }
            else if (hitter.collider.gameObject.layer == LayerMask.NameToLayer("Water")) { }
            else
            {
                //Debug.Log(hitter.collider.gameObject.name);
            }
        }
        
    }

}

