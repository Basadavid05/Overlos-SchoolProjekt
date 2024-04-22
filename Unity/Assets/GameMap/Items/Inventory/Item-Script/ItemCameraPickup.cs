using UnityEngine;

public class ItemCameraPickup : MonoBehaviour
{    
    private int itemLayer;
    private Pointers pointers;

    private void Start()
    {
        pointers = ItemPlacement.instance.Pointers;
        itemLayer = LayerMask.NameToLayer("Items");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == itemLayer)
        {
            pointers.ChangeOn();
        }
        else
        {
            pointers.ChangeBack();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == itemLayer)
        {
            pointers.ChangeOn();
        }
        else
        {
            pointers.ChangeBack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == itemLayer)
        {
            pointers.ChangeBack();
        }
    }
}
