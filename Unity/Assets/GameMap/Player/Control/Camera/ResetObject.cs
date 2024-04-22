using UnityEngine;

public class ResetObject : MonoBehaviour
{
    private void Start()
    {
        // Reset position to (0, 0, 0)
        transform.position = Vector3.zero;
        // Reset rotation to identity rotation
        transform.rotation = Quaternion.identity;
    }
}
