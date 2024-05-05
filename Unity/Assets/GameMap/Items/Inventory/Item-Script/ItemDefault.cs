using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class ItemDefault : MonoBehaviour
{
    [Header("Object Settings")]
    private Rigidbody objRigidbody;
    private Transform objTransform;
    private Collider coll;
    private Outline outlines;

    [Header("Bools")]
    public bool interactable;
    public bool PickedUp;

    [Header("Keys")]
    public KeyCode PickupKey;
    public KeyCode DropKey;

    [Header("Something")]
    private Item item;
    private bool polation=false;

    private void Start()
    {
        objRigidbody = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        objTransform = transform;
        outlines = GetComponent<Outline>();
        item=GetComponent<ItemTypesChanger>().item;
        polation = item.InventoryPlacement == ItemType.CanBeInToolbar && item.Toolbar;
        if (polation)
        {
            objRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            objRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        if (transform.tag=="Player")
        {
            PickItUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PickedUp && other.CompareTag("MainCamera"))
        {
            outlines.enabled = true;
            interactable = true;
        }
        else if (PickedUp && other.CompareTag("MainCamera"))
        {
            outlines.enabled = false;
            interactable = false;
            if (item.InventoryPlacement == Item.ItemType.CannotBeInToolbar)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (PickedUp && other.CompareTag("MainCamera"))
        {
            outlines.enabled = false;
            interactable = false;
            if (item.InventoryPlacement == Item.ItemType.CannotBeInToolbar)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
                outlines.enabled = false;
                interactable = false;
        }
    }


    private void Update()
    {
        if (Main.main.ChangedHotkeys) { 
            PickupKey = Main.main.keyMappings[Main.Hotkeys.interract];
            DropKey = Main.main.keyMappings[Main.Hotkeys.Drop];
        }
        if (interactable == true)
        {
            if (Input.GetKeyDown(PickupKey) && !ItemPlacement.instance.FullInv)
            {
                ItemPlacement.instance.Add(item);
                if (item.name == "Knife" && ThrowWeapon.TotalThrows<item.MaxItem+1)
                {
                    ThrowWeapon.TotalThrows++;
                }
                    PickItUp();

            }
        }
        if (PickedUp == true)
        {
            if (Input.GetKeyDown(DropKey) && item.name != "Knife")
            {
                DropItDown();
            }
        }
        if (transform.tag == ("Toolbar-Items"))
        {
            PickedUp = true;
        }
        polation = item.InventoryPlacement == ItemType.CanBeInToolbar && item.Toolbar;
    }

    private void PickItUp()
    {
        objTransform.parent = ItemPlacement.instance.ReplacePlace;
        ItemPlacement.instance.Pointers.ChangeBack();
        objTransform.localPosition = Vector3.zero;  // Reset local position in case it was modified elsewhere
        objTransform.localRotation = Quaternion.identity; // Reset local rotation in case it was modified elsewhere
        if (polation)
        {
            coll.enabled = false;
            objRigidbody.useGravity = false;
            if (gameObject.GetComponent<Projectile>() !=null)
            {
                objRigidbody.isKinematic = false;
            }
            else
            {
                objRigidbody.isKinematic = true;
            }
            objRigidbody.interpolation = RigidbodyInterpolation.None;
            //objRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            PickedUp = true;
            interactable = false;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    public void DropItDown()
    {
        if (polation)
        {
            coll.enabled = true;
            objRigidbody.useGravity = true;
            objRigidbody.isKinematic = false;
            //objRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            //objRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        objTransform.parent = ItemPlacement.instance.ItemDropLocation;
        objRigidbody.velocity = Camera.main.transform.forward * Time.deltaTime;
        ItemPlacement.instance.Remove(item);
        PickedUp = false;
    }

  
}
