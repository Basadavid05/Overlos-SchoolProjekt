using System.Collections;
using UnityEngine;

public class ResetRotation : MonoBehaviour
{
   private void Start()
    {
        // Reset rotation to identity rotation
        transform.rotation = Quaternion.identity;
    }
}
