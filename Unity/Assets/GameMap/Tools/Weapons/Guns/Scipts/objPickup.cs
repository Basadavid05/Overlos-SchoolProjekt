using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class objPickup : MonoBehaviour
{
    [Header("Crosshair")]
    public GameObject crosshair1, crosshair2;

    [Header("Object Settings")]
    private Rigidbody objRigidbody;
    private Transform objTransform;
    public Collider coll;
    private Outline outlines;


    [Header("Teleportation")]
    public Transform OriginalPoint;
    public Transform CameraPosition;

    [Header("Bools")]
    private bool interactable;
    private bool PickedUp;

    [Header("Throw")]
    private float throwAmount;

    [Header("Keys")]
    public KeyCode PickupKey = KeyCode.E;
    public KeyCode DropKey = KeyCode.O;

    [Header("Something")]
    public Item item;

    private void Start()
    {
        objRigidbody = GetComponent<Rigidbody>();
        coll=GetComponent<Collider>();
        objTransform = transform;
        outlines=GetComponent<Outline>();
    }

    void OnTriggerStay(Collider other)
    {
        if (!PickedUp && other.CompareTag("MainCamera"))
        {
            SetCrosshairVisibility(false, true);
            outlines.enabled = true;
            interactable = true;
            

        }
        else if (PickedUp && other.CompareTag("MainCamera"))
        {
            SetCrosshairVisibility(true, false);
            outlines.enabled = false;
            interactable = false;
            //Destroy(gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (PickedUp == false)
            {
                SetCrosshairVisibility(true, false);
                outlines.enabled = false;
                interactable = false;
            }
            if (PickedUp == true)
            {
                SetCrosshairVisibility(true, false);
                outlines.enabled = false;
                interactable = false;
            }
        }
    }

  
    private void Update()
    {
        if (interactable == true)
        {
            if (Input.GetKeyDown(PickupKey))
            {
                PickItUp();  
            }
        }
        if (PickedUp == true)
        {
            if (Input.GetKeyDown(DropKey))
            {
                Dropdown();
            }
        }
    }


    private void SetCrosshairVisibility(bool CrossHair1Status, bool CrossHair2Status)
    {
        crosshair1.SetActive(CrossHair1Status);
        crosshair2.SetActive(CrossHair2Status);
    }

    private void PickItUp()
    {
        coll.enabled = false;
        //objTransform.parent = instance.addItem().transform.parent;
        objTransform.localPosition = Vector3.zero;  // Reset local position in case it was modified elsewhere
        objTransform.localRotation = Quaternion.identity; // Reset local rotation in case it was modified elsewhere
        objRigidbody.useGravity = false;
        objRigidbody.isKinematic = true;
        //ItemSort.instance.Add(item);
        //ItemSort.instance.listItems();
        PickedUp = true;
        interactable = false;
    }
    public void Dropdown()
    {
        objTransform.parent = OriginalPoint;
        coll.enabled = true;
        objRigidbody.useGravity = true;
        objRigidbody.isKinematic = false;
        objRigidbody.velocity = CameraPosition.forward * throwAmount * Time.deltaTime;
        //ItemSort.instance.Remove(item);
        //ItemSort.instance.listItems();
        PickedUp = false;
        interactable = true;
    }
}