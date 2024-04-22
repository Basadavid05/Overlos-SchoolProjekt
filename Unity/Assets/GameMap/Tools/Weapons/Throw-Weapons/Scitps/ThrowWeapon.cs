using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowWeapon : MonoBehaviour
{
    [Header("References")]
    public GameObject objectToThrow;
    public Transform attacPoint;
    public Camera cam;
    public MoveControl Control;

    [Header("Settings")]
    public static float TotalThrows=3;
    private float ThrowCooldown=3f;

    [Header("Keycodes")]
    public KeyCode throwKey1= KeyCode.Mouse2;
    public KeyCode throwKey2= KeyCode.F;

    [Header("Thowning")]
    public float ThrownForce=20f;
    public float ThrowUpwardForce=5f;
    private bool readyToThrown;
    public Item item;

    private void Start()
    {
        Control=GetComponent<MoveControl>();
        TotalThrows = 3;
        readyToThrown = true;
        for (int i = 0; i < 3; i++)
        {
            ItemPlacement.instance.Add(item);
        }
    }

    private void Update()
    {

        if((Input.GetKeyDown(throwKey1) || Input.GetKeyDown(throwKey2)) && readyToThrown && TotalThrows > 0 && !Control.ISUnderwater)
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrown=false;


        GameObject projectile = Instantiate(objectToThrow, attacPoint.position + cam.transform.forward*2f, cam.transform.rotation);
        
        /*Vector3 rotationEuler = new Vector3(0f, 0f, 0f); // Adjust these values as needed
        projectile.transform.rotation = Quaternion.Euler(rotationEuler);*/


        Rigidbody projectileRB =projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        /*
        if (Physics.Raycast(cam.position,cam.forward,out hit,500f))
        {
            forceDirection = (hit.point - attacPoint.position).normalized;
        }*/

        //Vector3 forceToAdd = forceDirection * 10f + transform.up * ThrowUpwardForce;
        Vector3 forceToAdd = forceDirection * 15f + transform.up * 5f;

        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        TotalThrows--;
        for (int i = 0; i < ItemPlacement.instance.Inventorys.Count; i++)
        {
            if (ItemPlacement.instance.Inventorys[i].Itemname == "Knife")
            {
                if (ItemPlacement.instance.Inventorys[i].counter > 1)
                {
                    ItemPlacement.instance.Inventorys[i].counter--;
                }
                else
                {
                    ItemPlacement.instance.Inventorys[i].counter--;
                    Destroy(ItemPlacement.instance.Inventorys[i].gameObject);
                    ItemPlacement.instance.InventoryRemover(ItemPlacement.instance.Inventorys[i]);
                    ItemPlacement.instance.items.Remove(item);
                }
            }
        }

        //throwCooldown
        Invoke(nameof(ResetThrow), ThrowCooldown);
    }

    void ResetThrow()
    {
        readyToThrown=true;
    }


}